import { ProductsGrid, SectionTitle } from '.';

function FeaturedProducts() {
  return (
    <section className='pt-24'>
      <SectionTitle text='featured books' />
      <ProductsGrid />
    </section>
  );
}
export default FeaturedProducts;
