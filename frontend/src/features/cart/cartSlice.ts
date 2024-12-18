import { createSlice, PayloadAction, nanoid, createAsyncThunk } from '@reduxjs/toolkit';
import { type CartItem, type CartState } from '@/utils';
import { toast } from '@/components/ui/use-toast';
import { client } from '@/api/client';

const defaultState: CartState = {
  cartItems: [],
  numItemsInCart: 0,
  cartTotal: 0,
  shipping: 500,
  tax: 0,
  orderTotal: 0,
  id: '1234',
  status: 'idle',
  error: null
};

// type SliceState = {
//   cart: CartState;
//   // Multiple possible status enum values
//   status: 'idle' | 'loading' | 'succeeded' | 'failed',
//   error: string | null | undefined
// };

// const defaultState: SliceState = {
//   cart: defaultStateOld,
//   // {
//   //   cartItems: [],
//   //   numItemsInCart: 0,
//   //   cartTotal: 0,
//   //   shipping: 500,
//   //   tax: 0,
//   //   orderTotal: 0,
//   //   id: '1234',
//   // },
//   status: 'idle',
//   error: null
// }

const getCartFromLocalStorage = (): CartState => {
  const cart = localStorage.getItem('cart');
  return cart ? JSON.parse(cart) : defaultState;
};

export const cartSlice = createSlice({
  name: 'cart',
  initialState: getCartFromLocalStorage(),
  reducers: {
    addItem: (state, action: PayloadAction<CartItem>) => {
      const newCartItem = action.payload;
      const item = state.cartItems.find((i) => i.cartID === newCartItem.cartID);
      if (item) {
        item.amount += newCartItem.amount;
      } else {
        state.cartItems.push(newCartItem);
      }
      state.numItemsInCart += newCartItem.amount;
      state.cartTotal += Number(newCartItem.price) * newCartItem.amount;
      // state.tax = 0.1 * state.cartTotal;
      // state.orderTotal = state.cartTotal + state.shipping + state.tax;
      // localStorage.setItem('cart', JSON.stringify(state));
      cartSlice.caseReducers.calculateTotals(state);
      toast({ description: 'Item added to cart' });
    },
    clearCart: () => {
      localStorage.setItem('cart', JSON.stringify(defaultState));
      return defaultState;
    },
    removeItem: (state, action: PayloadAction<string>) => {
      const cartID = action.payload;
      const cartItem = state.cartItems.find((i) => i.cartID === cartID);
      if (!cartItem) return;
      state.cartItems = state.cartItems.filter((i) => i.cartID !== cartID);
      state.numItemsInCart -= cartItem.amount;
      state.cartTotal -= Number(cartItem.price) * cartItem.amount;
      cartSlice.caseReducers.calculateTotals(state);
      toast({ description: 'Item removed from the cart' });
    },
    editItem: (
      state,
      action: PayloadAction<{ cartID: string; amount: number }>
    ) => {
      const { cartID, amount } = action.payload;
      const cartItem = state.cartItems.find((i) => i.cartID === cartID);
      if (!cartItem) return;

      state.numItemsInCart += amount - cartItem.amount;
      state.cartTotal += Number(cartItem.price) * (amount - cartItem.amount);
      cartItem.amount = amount;

      cartSlice.caseReducers.calculateTotals(state);
      toast({ description: 'Amount Updated' });
    },
    calculateTotals: (state) => {
      state.tax = 0.1 * state.cartTotal;
      state.orderTotal = state.cartTotal + state.shipping + state.tax;
      localStorage.setItem('cart', JSON.stringify(state));
    },
  },
  // You can define your selectors here. These selectors receive the slice
  // state as their first argument.
  selectors: {
    selectNumItemsInCart: cart => cart.numItemsInCart,
    selectCartTotal: cart => cart.cartTotal,
    selectCartItems: cart => cart.cartItems,
    selectCart: cart => cart,
  },
  extraReducers(builder) {
    builder
      .addCase(fetchCart.pending, (state, action) => {
        state.status = 'loading'
      })
      .addCase(fetchCart.fulfilled, (state, action) => {
        state.status = 'succeeded'
        // Add any fetched posts to the array
        //state = action.payload;
        //state = action.payload;
        state.cartItems = action.payload.cartItems;
        state.numItemsInCart = action.payload.numItemsInCart;
        state.cartTotal = action.payload.cartTotal;
        state.shipping = action.payload.shipping;
        state.tax = action.payload.tax;
        state.orderTotal = action.payload.orderTotal;
      })
      .addCase(fetchCart.rejected, (state, action) => {
        state.status = 'failed'
        state.error = action.error.message
      })
  }  
  });
  
export const { addItem, clearCart, removeItem, editItem } = cartSlice.actions;

// Selectors returned by `slice.selectors` take the root state as their first argument.
export const { selectNumItemsInCart, selectCartTotal, selectCartItems, selectCart } = cartSlice.selectors

export const fetchCart = createAsyncThunk('cart/fetchCart', async () => {
  const response = await client.get('/fakeApi/cart');
  //console.log(response.data);
  return response.data
})
