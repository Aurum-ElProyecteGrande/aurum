"use client";
import MobileTransactionsPage from '@/components/mobile_view/mobile-transactions/MobileTransactions';
import Transactions from '@/components/transactions/transactions-page/Transactions';
import useDeviceDetect from '@/hook/useDeviceDetect';
 
export default function TransactionsPage() 
{
 const isTabletPortrait = useDeviceDetect();

  return (
    <>
    {
     isTabletPortrait ? <MobileTransactionsPage /> : <Transactions />
    }
    </>
  );
}