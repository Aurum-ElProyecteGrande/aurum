import { fetchBalance } from "@/scripts/dashboard_scripts/dashboard_scripts"
import { Ballet } from "next/font/google"
import { useEffect, useState } from "react"

export default function AccountBalances({ isEditMode, accounts }) {

    const [balances, setBalances] = useState([])

    useEffect(() => {
        const getBalances = async () => {
            let updatedBalances = await Promise.all(accounts.map(async (acc) => (
                {
                    name: acc.displayName, balance: await fetchBalance(acc.accountId)
                }))
            )
            setBalances(updatedBalances)
        }
        if (accounts) {
            getBalances()
        }
    }, [accounts])


    return (
        <div className="chart">
            <div className="chart-title">Account Balances</div>
            <div className="chart-body">
                <div className="account-balance-container">
                    {balances && balances.map(balance => (
                        <div key={balance.name} className="row">
                            <div>{balance.name}</div>
                            <div>{balance.balance.toLocaleString('hu-HU', { style: 'currency', currency: 'HUF' })}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}