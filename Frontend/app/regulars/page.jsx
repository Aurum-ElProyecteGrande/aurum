"use client";
import MobileRegularsPage from '@/components/mobile_view/mobile_regulars/mobileRegulars';
import Regulars from '@/components/regulars/regulars_page/Regulars';
import useDeviceDetect from '@/hook/useDeviceDetect';
 
export default function RegularsPage() 
{
 const isTabletPortrait = useDeviceDetect();

  return (
    <>
    {
     isTabletPortrait ? <MobileRegularsPage /> : <Regulars />
    }
    </>
      
 

  );
}