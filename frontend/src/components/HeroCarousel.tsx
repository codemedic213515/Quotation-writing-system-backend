import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel';
import { Card, CardContent } from '@/components/ui/card';

//import hero1 from '../assets/hero1.webp';
//import hero2 from '../assets/hero2.webp';
//import hero3 from '../assets/hero3.webp';
//import hero4 from '../assets/hero4.webp';
import hero1 from '../../public/images/51DmcUh8uwL._SY466_.jpg';
import hero2 from '../../public/images/61pDNU9qEGL._SY466_.jpg';
import hero3 from '../../public/images/81XP4hEXDXL._SY466_.jpg';
import hero4 from '../../public/images/91hYW0rTipL._SY466_.jpg';

const carouselImages = [hero1, hero2, hero3, hero4];

function HeroCarousel() {
  return (
    <div className='hidden lg:block'>
      <Carousel>
        <CarouselContent>
          {carouselImages.map((image, index) => {
            return (
              <CarouselItem key={index}>
                <Card>
                  <CardContent className='p-2'>
                    <img
                      src={image}
                      alt='hero'
                      className='w-full h-[24rem] rounded-md object-cover'
                    />
                  </CardContent>
                </Card>
              </CarouselItem>
            );
          })}
        </CarouselContent>
        <CarouselPrevious />
        <CarouselNext />
      </Carousel>
    </div>
  );
}
export default HeroCarousel;
