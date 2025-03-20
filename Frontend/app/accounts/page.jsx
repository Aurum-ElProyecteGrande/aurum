"use client";
import Accounts from '@/components/accounts/Accounts';
import useDeviceDetect from '@/hook/useDeviceDetect';

export default function TransactionsPage() {
    const isTabletPortrait = useDeviceDetect();

    return (
        <>
            {
                isTabletPortrait ? <></> : <Accounts />
            }
        </>
    );
}