"use client";

import Dashboard from '@/components/dashboard/dashboard-page/Dashboard';
import MobileDashboardPage from '@/components/mobile_view/mobile_dashboard_page/mobile-dashboard/MobileDashboard';
import useDeviceDetect from '@/hook/useDeviceDetect';
 
export default function DashboardPage() 
{
 const isTabletPortrait = useDeviceDetect();

  return (
    <>
    {
     isTabletPortrait ? <MobileDashboardPage /> : <Dashboard />
    }
    </>
      
 

  );
}