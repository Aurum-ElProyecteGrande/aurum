import { fetchBalance } from "@/scripts/dashboard_scripts/dashboard_scripts"
import { Ballet } from "next/font/google"
import { useEffect, useState } from "react"

export default function AccountBalances({ accounts }) {

    const [balances, setBalances] = useState([])

    useEffect(() => {
        const getBalances = async () => {
            let updatedBalances = await Promise.all(accounts.map(async (acc) => (
                {
                    name: acc.displayName, balance: await fetchBalance(acc.accountId), currency: acc.currency
                }))
            )

            updatedBalances = [...updatedBalances.sort((a, b) => b.balance - a.balance)]

            if (updatedBalances.length > 3) {
                let topBalances = []
                topBalances.push(topBalances[0], topBalances[1], topBalances[2])
                return setBalances(topBalances)
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
                            <div className="ballance">{balance.balance.toLocaleString('hu-HU', { style: 'currency', currency: balance.currency.currencyCode })}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}