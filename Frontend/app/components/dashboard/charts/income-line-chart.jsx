"use client"

import { useEffect, useState } from "react";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from "recharts";
import { fetchIncomeByDate, fetchAccounts } from "@/scripts/dashboard_scripts/dashboard_scripts";
import ChangeChartType from "./chart-utils/change-chart-type";
import ChangeDaysShown from "./chart-utils/change-days-shown";
import ChangeChartForm from '../change-chart-form';

export default function IncomeLineChart({ isEditMode, accounts, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts }) {

    const [incomesByDateString, setIncomesByDateString] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [chartData, setChartData] = useState([])
    const [rawChartData, setRawChartData] = useState([])
    const [curAccount, setCurAccount] = useState(accounts[0])
    const [daysShown, setDaysShown] = useState(10)

    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(nDaysAgo.getDate() - daysShown);
        setStartDate(nDaysAgo)
    }, [daysShown])

    useEffect(() => {
        if (accounts[0]) {
            setCurAccount(accounts[0])
        }
    }, [accounts])

    useEffect(() => {
        const getIncomes = async (accId) => {
            const updatedIncomes = await fetchIncomeByDate(accId, startDate.toISOString().slice(0, 10), today.toISOString().slice(0, 10))
            let incomesByDate = Object.groupBy(updatedIncomes, ({ date }) => date)
            let updatedIncomeByDateString = []
            for (const key in incomesByDate) {
                updatedIncomeByDateString[key.toString().slice(0, 10)] = incomesByDate[key]
            }
            setIncomesByDateString(updatedIncomeByDateString)
        }
        if (curAccount) {
            getIncomes(curAccount.accountId)
        }
    }, [startDate, curAccount])

    useEffect(() => {
        let updatedChartData = []
        for (let i = 0; i <= daysShown; i++) {
            let curDate = new Date(startDate)
            curDate.setDate(startDate.getDate() + i)
            let dateString = curDate.toISOString().slice(0, 10)


            if (incomesByDateString[dateString]) {
                let sum = 0
                for (const income of incomesByDateString[dateString]) {
                    sum += income.amount
                }
                updatedChartData[dateString] = {
                    name: dateString,
                    income: sum
                }
            } else {
                updatedChartData[dateString] = {
                    name: dateString,
                    income: 0
                }
            }
        }
        setChartData(updatedChartData)
    }, [startDate, incomesByDateString])


    useEffect(() => {
        let updatedRowChartData = []
        for (const key in chartData) {
            updatedRowChartData.push(chartData[key])
        }
        setRawChartData(updatedRowChartData)
    }, [chartData])

    const handleChangeType = (e) => {
        const accName = e.target.value
        const updatedCurAcc = accounts.find(a => a.displayName == accName)
        setCurAccount(updatedCurAcc)
    }

    const handleChangeDays = (e) => {
        setDaysShown(e.target.value)
    }

    console.log("inc",rawChartData)

    return (

        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>

            <div className='chart-title-container'>
                {isEditMode &&
                    <div className="change-chart-types-container">
                        <ChangeChartType handleChangeType={handleChangeType} accounts={accounts} curAccount={curAccount} />
                        <ChangeDaysShown handleChangeDays={handleChangeDays} daysShown={daysShown} />
                        <ChangeChartForm
                            choosenCharts={choosenCharts}
                            segmentIndex={segmentIndex}
                            possibleCharts={possibleChartsBySegment[segmentIndex]}
                            setChoosenCharts={setChoosenCharts} />
                    </div>
                }
                <div className="chart-title">
                    <p>Incomes in last {daysShown} days #{curAccount && curAccount.displayName}</p>
                </div>
            </div>

            <div className="chart">

                <ResponsiveContainer width="100%" height={200} className="chart-body">
                    <LineChart data={rawChartData}>
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="name" />
                        <YAxis />
                        <Tooltip
                            contentStyle={{ backgroundColor: "#333333", borderColor: "#F9D342", color: "#F4F4F4" }}
                        />
                        <Legend />
                        <Line type="monotone" dataKey="income" stroke="#F9D342" name={`${curAccount.displayName} (${curAccount.currency.currencyCode})`}/>
                    </LineChart>
                </ResponsiveContainer>
            </div>

        </div>
    )
}