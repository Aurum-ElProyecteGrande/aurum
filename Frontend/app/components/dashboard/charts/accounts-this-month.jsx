import { useEffect, useState } from 'react';
import ChangeChartForm from '../change-chart-form';
import { displayCurrency, fetchExpensesByDate, fetchIncomeByDate } from '@/scripts/dashboard_scripts/dashboard_scripts';


export default function AccountsThisMonth({ isEditMode, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts, accounts, expenses, incomes }) {

    const [accountsWithTotals, setAccountsWithTotals] = useState([])
    const firstDayOfMonth = new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().slice(0, 10);
    const today = new Date().toISOString().slice(0, 10)

    useEffect(() => {
        const getTrxs = async () => {
            const acc1Exp = await fetchExpensesByDate(accounts[0].accountId, firstDayOfMonth, today)
            const acc1Inc = await fetchIncomeByDate(accounts[0].accountId, firstDayOfMonth, today)
            let acc2Exp = []
            let acc2Inc = []
            let acc3Exp = []
            let acc3Inc = []
            if (accounts[1]) {
                acc2Exp = await fetchExpensesByDate(accounts[1].accountId, firstDayOfMonth, today)
                acc2Inc = await fetchIncomeByDate(accounts[1].accountId, firstDayOfMonth, today)
            }
            if (accounts[2]) {
                acc3Exp = await fetchExpensesByDate(accounts[2].accountId, firstDayOfMonth, today)
                acc3Inc = await fetchIncomeByDate(accounts[2].accountId, firstDayOfMonth, today)
            }

            const updatedAccsWithTrxs = [
                { name: accounts[0].displayName, inc: acc1Inc, exp: acc1Exp, currencyCode: accounts[0].currency.currencyCode },
                accounts[1] && { name: accounts[1].displayName, inc: acc2Inc, exp: acc2Exp, currencyCode: accounts[1].currency.currencyCode},
                accounts[2] && { name: accounts[2].displayName, inc: acc3Inc, exp: acc3Exp, currencyCode: accounts[2].currency.currencyCode}
            ]


            updatedAccsWithTrxs.forEach(acc => {
                acc.inc = acc.inc.reduce((a, c) => a += c.amount, 0)
                acc.exp = acc.exp.reduce((a, c) => a += c.amount, 0)
            });

            setAccountsWithTotals(updatedAccsWithTrxs)

        }
        if (accounts[0])
            getTrxs()
    }, [accounts])

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
                    <p>Ballance changes this month</p>
                </div>
            </div>
            <div className="chart">
                <div className="account-this-month">
                    {accountsWithTotals && accountsWithTotals.map(acc => (
                        <div key={acc.name} className='column'>
                            <div className='acc-name'> {acc.name}</div>
                            <div className='row exp'>
                                <div >Total expense</div>
                                <div className='exp'>- {displayCurrency(acc.exp, acc.currencyCode)}</div>
                            </div>
                            <div className='row inc'>
                                <div>Total income</div>
                                <div  className='inc'>+ {displayCurrency(acc.exp, acc.currencyCode)}</div>
                            </div>
                            <div className={`row total${acc.inc - acc.exp > 0 ? "inc" : "exp"}`}>
                                <div className={acc.inc - acc.exp > 0 ? "inc" : "exp"}>Total sum</div>
                                <div className={acc.inc - acc.exp > 0 ? "inc" : "exp"}>{displayCurrency(acc.inc - acc.exp, acc.currencyCode)}</div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>

        </div>
    )
}