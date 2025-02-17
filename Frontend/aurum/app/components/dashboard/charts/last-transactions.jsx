import { fetchExpenses, fetchIncome } from "@/scripts/dashboard_scripts/dashboard_scripts"
import { useEffect, useState } from "react"

export default function LastTransactions({ accounts, expenses, incomes }) {

    const [transactions, setTransactions] = useState([])
    const maxTransactions = 15


    useEffect(() => {
        const getTransactions = async () => {
            let updatedExpenses = [...expenses]
            updatedExpenses = updatedExpenses.map(e => {
                return {
                    ...e,
                    isExpense: true
                }
            })

            let updatedIncomes = [...incomes]
            updatedIncomes = updatedIncomes.map(i => {
                return {
                    ...i,
                    isExpense: false
                }
            })

            const updatedTransactions = [...updatedExpenses, ...updatedIncomes]

            setTransactions(sortAndFilterTransactions(updatedTransactions))
        }
        if (expenses[0] && incomes[0]) {
            getTransactions()
        }
    }, [expenses, incomes])

    const sortAndFilterTransactions = (txs) => {
        let allTransactions = [...txs]
        allTransactions = allTransactions.sort((a, b) => new Date(a.date) - new Date(b.date))

        let updatedTransactions = []
        for (let i = 0; i < maxTransactions; i++) {
            updatedTransactions.push(allTransactions[i])
        }
        updatedTransactions = updatedTransactions.reverse()
        return updatedTransactions
    }

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