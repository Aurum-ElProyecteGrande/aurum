"use client";
import React, { useState, useEffect } from "react";
import { fetchAccounts, fetchBalance } from "@/scripts/dashboard_scripts/dashboard_scripts";
import MobileBalanceChart from "@/components/mobile_dashboard_page/balance_chart/MobileBalanceChart";
import { colors } from "@/scripts/mobile_dashboard_scripts/mobile_dashboard";
import MobileBottomBar from "@/components/mobile_dashboard_page/mobile_bottombar/MobileBottomBar";

const MobileDashboardPage = () => {
    const [accounts, setAccounts] = useState([]);
    const [fullData, setFullData] = useState([]);
    const [loading, setLoading] = useState(true);

    const getAccounts = async () => {
        const updatedAccounts = await fetchAccounts()

        setAccounts(updatedAccounts)
    }

    const getData = async () => {
        try {
            setLoading(true)
            
            const promises = accounts.map((acc) =>
                fetchBalance(acc.accountId).then((balance) => ({
                    account: acc,
                    balance: balance,
                }))
            );

            const mergedData = await Promise.all(promises);

            setFullData(mergedData);

            const startTime = Date.now();
            const elapsedTime = Date.now() - startTime;
            
            if (elapsedTime < 2000) {
                await new Promise((resolve) => setTimeout(resolve, 2000 - elapsedTime));
            }
        } catch (error) {
            console.error("Error fetching data:", error);
        }
        finally {
            setLoading(false)
        }
    };

    useEffect(() => {
        getAccounts()
    }, []);

    useEffect(() => {
        getData();

    }, [accounts]);

    return (
        <section className="mobile-dashboard">
            <MobileBottomBar/>
            <div className="mobile-dashboard-chart">
                <h1>Account Balances Overview</h1>
            {fullData.length > 0 ? (
                    <MobileBalanceChart data={fullData} colors={colors} />
                ) : (
                    <p>Loading...</p>
                )}
            </div>
            <div className="mobile-dashboard-cards">
                {fullData.map((data, index) =>
                    <div key={data.account.accountId} className="mobile-dashboard-card">
                        <h1 style={{color: colors[index % colors.length]}}>{data.account.displayName}</h1>
                        <span>{data.balance} {data.account.currency.symbol}</span>
                    </div>)}

            </div>
        </section>

    );
}


export default MobileDashboardPage