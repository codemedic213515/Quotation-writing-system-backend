import axios from 'axios';
import { ProductsResponse, OrdersResponse, SingleProductResponse } from '@/utils/types';
import { client } from '@/api/client';

/* 
  Original code using axios
*/
//const productionUrl = 'https://strapi-store-server.onrender.com/api';

// export const customFetch = axios.create({
//   baseURL: productionUrl,
// });

/*
  Using the fake API
*/
const productionUrl = '/fakeApi';
//const productionUrl = 'http://localhost:5246/api';

export const customFetch = async <T>(url, params?): Promise<{data: T, headers: any}> => {
//export const customFetch = <T>(url, params): {data: T, headers: any} => {
  //let value = JSON.stringify(params.params);
  //let queryParams = new URLSearchParams(value).toString();
  //console.log(queryParams);

  let queryString = '';
  if (params) {
    queryString = JSON.stringify(params.params).replaceAll(':', '=');
    queryString = queryString.replaceAll('"', '');
    queryString = queryString.replaceAll(',', '&');
    queryString = queryString.replaceAll('{', '');
    queryString = queryString.replaceAll('}', '');
    if (queryString.length > 0) queryString = '?' + queryString;
  }
  const headers: any = '';
  try {
    const response = await client.get(productionUrl + url + queryString);
    console.log(response);
    return response;
  }
  catch (err) {
    // Did an error occur in the SingleProduct page? */
    if (url.indexOf('/products/') !== -1) return { data: blankSingleProductResponse as T, headers };
    return { data: blankResponse as T, headers };
  }
}

customFetch.get = async <T>(url, params): Promise<{data: T, headers: any}> => {
  const headers: any = '';
  try {
    const response = await client.get(productionUrl + url, params);
    return response;
  }
  catch (err) {
    return { data: blankResponse as T, headers };    
  }
}

const blankResponse: ProductsResponse =  {
  "data": [], 
  "meta": { 
    "pagination": {
      "page": 0,
      "pageSize": 10,
      "pageCount": 0,
      "total": 0
    }, 
    "categories": ["all"], 
    "companies": ["all"]
  }
};

const blankSingleProductResponse: SingleProductResponse =  {
  "data": {id: 0, attributes: {category: '', company: '', createdAt: '', description: '', featured: false, image: '', price: '', publishedAt: '', shipping: false, title: '', updatedAt: '', colors: []}}, 
  "meta": { 
    "pagination": {
      "page": 0,
      "pageSize": 10,
      "pageCount": 0,
      "total": 0
    }, 
    "categories": ["all"], 
    "companies": ["all"]
  }
};


