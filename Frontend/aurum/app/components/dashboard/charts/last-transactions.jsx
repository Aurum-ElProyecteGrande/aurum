import { fetchExpenses, fetchIncome } from "@/scripts/dashboard_scripts/dashboard_scripts"
import { trackSynchronousPlatformIOAccessInDev } from "next/dist/server/app-render/dynamic-rendering"
import { useEffect, useState } from "react"

export default function LastTransactions({ isEditMode, accounts }) {

    const [expenses, setExpenses] = useState([])
    const [incomes, setIncomes] = useState([])
    const [transactions, setTransactions] = useState([])
    const maxTransactions = 15


    useEffect(() => {
        const getExpenses = async () => {
            let updatedExpenses = []
            await Promise.all(accounts.map(async (acc) => {
                updatedExpenses.push(...await fetchExpenses(acc.accountId))
            }))
            updatedExpenses = updatedExpenses.map(e => {
                return {
                    ...e,
                    isExpense: true
                }
            })
            setExpenses(updatedExpenses)
        }

        const getIncomes = async () => {
            let updatedIncomes = []
            await Promise.all(accounts.map(async (acc) => {
                updatedIncomes.push(...await fetchIncome(acc.accountId))
            }))
            updatedIncomes = updatedIncomes.map(i => {
                return {
                    ...i,
                    isExpense: false
                }
            })
            setIncomes(updatedIncomes)
        }
        if (accounts) {
            getExpenses()
            getIncomes()
        }
    }, [accounts])

    useEffect(() => {
        let allTransactions = []
        allTransactions.push(...expenses)
        allTransactions.push(...incomes)
        allTransactions = allTransactions.sort((a, b) => new Date(a.date) - new Date(b.date))

        let updatedTransactions = []
        for (let i = 0; i < maxTransactions; i++) {
            updatedTransactions.push(allTransactions[i])
        }

        updatedTransactions = updatedTransactions.reverse()

        setTransactions(updatedTransactions)
    }, [expenses, incomes])

    return (
        <div className="chart">
            <div className="chart-title">
                <p>Last transactions</p>
            </div>
            <div className="chart-body">
                <div className="transaction-container">
                    {transactions[0] && transactions.map((t, i) => (
                        <div key={i} className={`row`}>
                            <div className={`date ${t.isExpense ? "expense" : "income"}`}>{t.date.toString().slice(0, 10)}</div>
                            <div className="label">
                                <p className={`${t.isExpense ? "expense" : "income"}`}>
                                    {t.label}
                                </p>
                            </div>
                            <div className={`amount ${t.isExpense ? "expense" : "income"}`}>
                                {t.isExpense ? "-" : "+"} {t.amount.toLocaleString('hu-HU', { style: 'currency', currency: 'HUF' })}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div >
    )
}