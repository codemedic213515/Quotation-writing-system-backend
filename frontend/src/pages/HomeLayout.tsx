import { Header, Loading, Navbar } from '@/components';
import { Outlet, useNavigation } from 'react-router-dom';


///////////////////////////////////////////////////////////////////////////////////////////////
//import { selectCart, fetchCart } from '@/features/cart/cartSlice';
//import React, { useEffect } from 'react'
//import { useAppDispatch, useAppSelector } from '@/app/hooks';
///////////////////////////////////////////////////////////////////////////////////////////////


function HomeLayout() {
  const navigation = useNavigation();
  const isPageLoading = navigation.state === 'loading';


  ///////////////////////////////////////////////////////////////////////////////////////////////
  // const dispatch = useAppDispatch();
  // const cart = useAppSelector(selectCart)
  // const cartStatus = useAppSelector(state => state.cart.status)

  // useEffect(() => {
  //   if (cartStatus === 'idle') {
  //     dispatch(fetchCart())
  //   }
  // }, [cartStatus, dispatch])


  //console.log('in HomeLayout');
  //console.log(cart);
  ///////////////////////////////////////////////////////////////////////////////////////////////

  
  return (
    <>
      <Header />
      <Navbar />
      <div className='align-element py-20'>
        {isPageLoading ? <Loading /> : <Outlet />}
      </div>
    </>
  );
}
export default HomeLayout;
