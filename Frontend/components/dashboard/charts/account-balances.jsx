import { displayCurrency, fetchBalance } from "@/scripts/dashboard_scripts/dashboard_scripts"
import { useEffect, useState } from "react"
import ChangeChartForm from '../change-chart-form';

export default function AccountBalances({ isEditMode, accounts, segmentIndex, chosenLayout, chosenCharts, possibleChartsBySegment, setChosenCharts }) {

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

        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>

            <div className='chart-title-container'>
                {isEditMode &&
                    <div className="change-chart-types-container">
                        <ChangeChartForm
                            chosenCharts={chosenCharts}
                            segmentIndex={segmentIndex}
                            possibleCharts={possibleChartsBySegment[segmentIndex]}
                            setChosenCharts={setChosenCharts} />
                    </div>
                }
                <div className="chart-title">
                    <p>Account balances</p>
                </div>
            </div>
            <div className="chart">
                <div className="chart-body">
                    <div className="account-balance-container">
                        {balances && balances.map(balance => (
                            <div key={balance.name} className="row">
                                <div>{balance.name}</div>
                                <div className="ballance">{displayCurrency(balance.balance, balance.currency.currencyCode)}</div>
                            </div>
                        ))}
                    </div>
                </div>


            </div>
        </div>
    )
}