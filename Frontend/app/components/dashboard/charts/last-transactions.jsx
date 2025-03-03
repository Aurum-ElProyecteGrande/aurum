import { displayCurrency } from '@/scripts/dashboard_scripts/dashboard_scripts';
import ChangeChartForm from '../change-chart-form';
import { useEffect, useState } from "react"

export default function LastTransactions({ isEditMode, accounts, expenses, incomes, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts }) {

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

    console.log(transactions)

    return (

        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>

            <div className='chart-title-container'>
                {isEditMode &&
                    <div className="change-chart-types-container">
                        <ChangeChartForm
                            choosenCharts={choosenCharts}
                            segmentIndex={segmentIndex}
                            possibleCharts={possibleChartsBySegment[segmentIndex]}
                            setChoosenCharts={setChoosenCharts} />
                    </div>
                }
                <div className="chart-title">
                    <p>Last transactions</p>
                </div>
            </div>

            <div className="chart">

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
                                    {t.isExpense ? "-" : "+"} {displayCurrency(t.amount, t.currency.currencyCode)}
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div >

        </div>
    )
}