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

            if (updatedBalances.length > 3) {
                const topBalances = [...updatedBalances.sort((a, b) => a.balance - b.balance)]
                updatedBalances = []
                updatedBalances.push(topBalances[0], topBalances[1], topBalances[2])
            }

            setBalances(updatedBalances)
        }
        if (accounts) {
            getBalances()
        }
    }, [accounts])


    return (
        <div className="chart">
            <div className="chart-title">
                <p>Account ballances</p>
            </div>
            <div className="chart-body">
                <div className="account-balance-container">
                    {balances && balances.map(balance => (
                        <div key={balance.name} className="row">
                            <div>{balance.name}</div>
                            <div className="ballance">{balance.balance.toLocaleString('hu-HU', { style: 'currency', currency: 'HUF' })}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}