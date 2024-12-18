import { http, HttpResponse } from 'msw'
import { setupWorker } from 'msw/browser'
import { factory, oneOf, manyOf, primaryKey } from '@mswjs/data'
import { nanoid } from '@reduxjs/toolkit'
import { faker } from '@faker-js/faker'
import { Server as MockSocketServer } from 'mock-socket'

import { parseISO } from 'date-fns';

//////////////////////////////////////////////////////////////////////////////////////
// Load data from json files when using fakeApi
//import products from '@/api/data/products.json';
import products from '@/api/data/books.json';
import users from '@/api/data/user.json';
import orders from '@/api/data/orders.json';
//////////////////////////////////////////////////////////////////////////////////////


const NUM_USERS = 3
const POSTS_PER_USER = 3
const RECENT_NOTIFICATIONS_DAYS = 7

// Add an extra delay to all endpoints, so loading spinners show up.
const ARTIFICIAL_DELAY_MS = 500; //2000

function delay(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

/* RNG setup */

// Set up a seeded random number generator, so that we get
// a consistent set of users / entries each time the page loads.
// This can be reset by deleting this localStorage value,
// or turned off by setting `useSeededRNG` to false.
let useSeededRNG = true

if (useSeededRNG) {
  let randomSeedString = localStorage.getItem('randomTimestampSeed')
  let seedDate

  if (randomSeedString) {
    seedDate = new Date(randomSeedString)
  } else {
    seedDate = new Date()
    randomSeedString = seedDate.toISOString()
    localStorage.setItem('randomTimestampSeed', randomSeedString)
  }

  faker.seed(seedDate.getTime())
}

function getRandomInt(min, max) {
  return faker.number.int({ min, max })
}

const randomFromArray = (array) => {
  const index = getRandomInt(0, array.length - 1)
  return array[index]
}

/* 

  MSW Data Model Setup 
  Check Github readme file for reference: https://github.com/mswjs/data

*/

export const db = factory({
  user: {
    id: primaryKey(nanoid),
    firstName: String,
    lastName: String,
    name: String,
    username: String,
    posts: manyOf('post'),
  },
  post: {
    id: primaryKey(nanoid),
    title: String,
    date: String,
    content: String,
    reactions: oneOf('reaction'),
    comments: manyOf('comment'),
    user: oneOf('user'),
  },
  comment: {
    id: primaryKey(String),
    date: String,
    text: String,
    post: oneOf('post'),
  },
  reaction: {
    id: primaryKey(nanoid),
    thumbsUp: Number,
    hooray: Number,
    heart: Number,
    rocket: Number,
    eyes: Number,
    post: oneOf('post'),
  },
  cartItem: {
    cartID: primaryKey(nanoid),
    productID: Number,
    image: String,
    title: String,
    price: String,
    amount: Number,
    productColor: String,
    company: String,
  },
  cart: {
    id: primaryKey(nanoid),
    numItemsInCart: Number,
    cartTotal: Number,
    shipping: Number,
    tax: Number,
    orderTotal: Number,
    cartItems: manyOf('cartItem'),
    status: String,
    error: String,    
  },
})

////////////////////////////////////////////////////////////////////////////////////////////////////////
// Creating fake cart data
const createCartData = () => {
  return {
    numItemsInCart: 2,
    cartTotal: 97768,
    shipping: 10,
    tax: 9777,
    orderTotal: 207545,
    cartItems: [
      db.cartItem.create(createCartItemData('images/pexels-photo-3679601.jpeg', 'Fake title', '9999', 2, 'blue', 'Amazon')),
      db.cartItem.create(createCartItemData('images/pexels-photo-1034584.jpeg', 'Fake title 2', '12345', 4, 'blue', 'Amazon')),
      db.cartItem.create(createCartItemData('images/pexels-photo-943150.jpeg', 'Fake title 3', '5678', 5, 'yellow', 'Canadian Tire')),
    ]
  }
}

const createCartItemData = (image, title, price, amount, productColor, company) => {
  return {
    productID: primaryKey(nanoid),
    image: image,
    title: title,
    price: price,
    amount: amount,
    productColor: productColor,
    company: company,
  }
}

db.cart.create(createCartData());
///////////////////////////////////////////////////////////////////////////////////////////////////////


const createUserData = () => {
  const firstName = faker.person.firstName()
  const lastName = faker.person.lastName()

  return {
    firstName,
    lastName,
    name: `${firstName} ${lastName}`,
    username: faker.internet.userName(),
  }
}

const createPostData = (user) => {
  return {
    title: faker.lorem.words(),
    date: faker.date.recent({ days: RECENT_NOTIFICATIONS_DAYS }).toISOString(),
    user,
    content: faker.lorem.paragraphs(),
    reactions: db.reaction.create(),
  }
}

// Create an initial set of users and posts
for (let i = 0; i < NUM_USERS; i++) {
  const author = db.user.create(createUserData())

  for (let j = 0; j < POSTS_PER_USER; j++) {
    const newPost = createPostData(author)
    db.post.create(newPost)
  }
}

const serializePost = (post) => ({
  ...post,
  user: post.user.id,
})


/* MSW REST API Handlers */

export const handlers = [
  /* Bookstore related API handlers start here */
  http.get('/fakeApi/auth/local', async function ({ params }) {
    return HttpResponse.json(user)
  }),  
  http.post('/fakeApi/auth/local', async function ({ request }) {
    const data = await request.json();
    const user = users.find(c => c.user.email == data.identifier.toLowerCase());
    //console.log(user);
    return HttpResponse.json(user)
  }),   
  http.get('/fakeApi/cart', async function () {
    const cart = db.cart.findFirst({
      where: { numItemsInCart: { equals: 2 } },
    }); // not sure how to select the firs record in the cart table. That is why I picked numItemsInCart = 2
    //await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(cart)
  }),
//   http.post('/fakeApi/products', async function () {
// console.log('this is a post');
//     return HttpResponse.json(products)
//   }),  
  http.get('/fakeApi/products', async ({ request, params }) => {
    const url = new URL(request.url);

    // Product search parameters
    const search = url.searchParams.get('search');
    const category = url.searchParams.get('category');
    const company = url.searchParams.get('company');
    const order = url.searchParams.get('order');
    const price = url.searchParams.get('price');
    const shipping = url.searchParams.get('shipping');
    const featuredOnly = url.searchParams.get('featured');
    let page = url.searchParams.get('page');

    let productList = products;
    if (featuredOnly) {
      productList = { data: products.data.filter(c => c.attributes.featured == true), meta: products.meta };
    }

    //http://localhost:5173/fakeApi/products?search=lamp&category=Tables&company=Artifex&order=high&price=74000&shipping=on
    if (search) {
      productList = { data: productList.data.filter(c => c.attributes.title.search(search) > 0), meta: products.meta };  
    }
    if (company) {
      if (company != 'all') {
        productList = { data: productList.data.filter(c => c.attributes.company == company), meta: products.meta };  
      }
    }
    if (category) {
      if (category != 'all') {
        productList = { data: productList.data.filter(c => c.attributes.category == category), meta: products.meta };  
      }
    }
    if (shipping) {
      if (shipping == 'on') {
        productList = { data: productList.data.filter(c => c.attributes.shipping == true), meta: products.meta };  
      }
    }
    if (price) {
        productList = { data: productList.data.filter(c => parseInt(c.attributes.price) <= price), meta: products.meta };  
    }

    switch (order) {
        case 'a-z':
          //(a < b) ? -1 : ((a > b) ? 1 : 0)
          productList = { data: productList.data.sort(function(a, b){return (
            (a.attributes.title < b.attributes.title) ? -1 : ((a.attributes.title > b.attributes.title) ? 1 : 0))
          }), meta: products.meta  };
          break;
        case 'z-a':
          productList = { data: productList.data.sort(function(a, b){return (
            (a.attributes.title > b.attributes.title) ? -1 : ((a.attributes.title < b.attributes.title) ? 1 : 0))
          }), meta: products.meta  };
          break;
        case 'high':
          productList = { data: productList.data.sort(function(a, b){return b.attributes.price - a.attributes.price}), meta: products.meta  };
          break;
        case 'low':
          productList = { data: productList.data.sort(function(a, b){return a.attributes.price - b.attributes.price}), meta: products.meta  };
          break;
        default:
          productList = { data: productList.data.sort(function(a, b){return (
            (a.attributes.title < b.attributes.title) ? -1 : ((a.attributes.title > b.attributes.title) ? 1 : 0))
          }), meta: products.meta  };
      }


    // pagination
    if (page == null) {
      page = 1;
    }
    productList.meta.pagination.total = productList.data.length;
    productList.meta.pagination.pageCount = 0
    if (productList.data.length > 0) {
      productList.meta.pagination.pageCount = Math.ceil(productList.data.length / productList.meta.pagination.pageSize);
    }
    productList.meta.pagination.page = page;
    productList = { data: productList.data.slice((page - 1) * 10, page * 10), meta: productList.meta };    

    //await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(productList)
  }),
  http.get('/fakeApi/products/:id', async function ({ params }) {
    const meta = products.meta;
    const product = products.data.find(c => c.id == params.id);
    //await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json({ data: {...product}, meta })
  }),  
  http.get('/fakeApi/orders', async ({ params }) => {
    //await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(orders)
  }),
  http.post('/fakeApi/orders', async function ({ request }) {
    const data = await request.json()

    //console.log('data on server');
    //console.log(data);
    
    if (data.content === 'error') {
      await delay(ARTIFICIAL_DELAY_MS)

      return new HttpResponse(
        JSON.stringify('Server error saving this post!'),
        {
          status: 500,
        },
      )
    }

    const newOrder = {
      id: getRandomInt(10000, 100000),
      attributes: {...data.data, createdAt: "2024-07-22T18:41:15.987Z", updatedAt: "2024-07-22T18:41:15.987Z", publishedAt: "2024-07-22T18:41:15.980Z"},
    };
    orders.data.push(newOrder);
    //console.log('new orders list');
    //console.log(orders);
    return HttpResponse.json(orders)  
  }),
  /* Bookstore related API handlers end here */




  http.get('/fakeApi/posts', async function () {
    const posts = db.post.getAll().map(serializePost)
    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(posts)
  }),
  http.post('/fakeApi/posts', async function ({ request }) {
    const data = await request.json()

    if (data.content === 'error') {
      await delay(ARTIFICIAL_DELAY_MS)

      return new HttpResponse(
        JSON.stringify('Server error saving this post!'),
        {
          status: 500,
        },
      )
    }

    data.date = new Date().toISOString()

    const user = db.user.findFirst({ where: { id: { equals: data.user } } })
    data.user = user
    data.reactions = db.reaction.create()

    const post = db.post.create(data)
    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(serializePost(post))
  }),
  http.get('/fakeApi/posts/:postId', async function ({ params }) {
    const post = db.post.findFirst({
      where: { id: { equals: params.postId } },
    })
    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(serializePost(post))
  }),
  http.patch('/fakeApi/posts/:postId', async ({ request, params }) => {
    const { id, ...data } = await request.json()
    const updatedPost = db.post.update({
      where: { id: { equals: params.postId } },
      data,
    })
    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(serializePost(updatedPost))
  }),

  http.get('/fakeApi/posts/:postId/comments', async ({ params }) => {
    const post = db.post.findFirst({
      where: { id: { equals: params.postId } },
    })

    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json({ comments: post.comments })
  }),

  http.post('/fakeApi/posts/:postId/reactions', async ({ request, params }) => {
    const postId = params.postId
    const { reaction } = await request.json()
    const post = db.post.findFirst({
      where: { id: { equals: postId } },
    })

    const updatedPost = db.post.update({
      where: { id: { equals: postId } },
      data: {
        reactions: {
          ...post.reactions,
          [reaction]: (post.reactions[reaction] += 1),
        },
      },
    })

    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(serializePost(updatedPost))
  }),
  http.get('/fakeApi/notifications', async () => {
    const numNotifications = getRandomInt(1, 5)

    let notifications = generateRandomNotifications(
      undefined,
      numNotifications,
      db,
    )

    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(notifications)
  }),
  http.get('/fakeApi/users', async () => {
    await delay(ARTIFICIAL_DELAY_MS)
    return HttpResponse.json(db.user.getAll())
  }),
]

export const worker = setupWorker(...handlers)
// worker.printHandlers() // Optional: nice for debugging to see all available route handlers that will be intercepted

/* Mock Websocket Setup */

const socketServer = new MockSocketServer('ws://localhost')

let currentSocket

const sendMessage = (socket, obj) => {
  socket.send(JSON.stringify(obj))
}

// Allow our UI to fake the server pushing out some notifications over the websocket,
// as if other users were interacting with the system.
const sendRandomNotifications = (socket, since) => {
  const numNotifications = getRandomInt(1, 5)

  const notifications = generateRandomNotifications(since, numNotifications, db)

  sendMessage(socket, { type: 'notifications', payload: notifications })
}

export const forceGenerateNotifications = (since) => {
  sendRandomNotifications(currentSocket, since)
}

socketServer.on('connection', (socket) => {
  currentSocket = socket

  socket.on('message', (data) => {
    const message = JSON.parse(data)

    switch (message.type) {
      case 'notifications': {
        const since = message.payload
        sendRandomNotifications(socket, since)
        break
      }
      default:
        break
    }
  })
})

/* Random Notifications Generation */

const notificationTemplates = [
  'poked you',
  'says hi!',
  `is glad we're friends`,
  'sent you a gift',
]

function generateRandomNotifications(since, numNotifications, db) {
  const now = new Date()
  let pastDate

  if (since) {
    pastDate = parseISO(since)
  } else {
    pastDate = new Date(now.valueOf())
    pastDate.setMinutes(pastDate.getMinutes() - 15)
  }

  // Create N random notifications. We won't bother saving these
  // in the DB - just generate a new batch and return them.
  const notifications = [...Array(numNotifications)].map(() => {
    const user = randomFromArray(db.user.getAll())
    const template = randomFromArray(notificationTemplates)
    return {
      id: nanoid(),
      date: faker.date.between({ from: pastDate, to: now }).toISOString(),
      message: template,
      user: user.id,
    }
  })

  return notifications
}