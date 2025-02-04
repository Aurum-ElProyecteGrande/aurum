"use client"

import { useEffect, useState } from "react";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from "recharts";
import { fetchIncomeByDate, fetchAccounts } from "@/scripts/dashboard_scripts/dashboard_scripts";


export default function IncomeLineChart({ isEditMode }) {

    const [incomesByDateString, setIncomesByDateString] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [chartData, setChartData] = useState([])
    const [rawChartData, setRawChartData] = useState([])
    const [accounts, setAccounts] = useState([])
    const [curAccount, setCurAccount] = useState()
    const daysShown = 10

    const userId = 1 //FROM CREDENTIALS probably?

    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(nDaysAgo.getDate() - daysShown);
        setStartDate(nDaysAgo)
    }, [])

    useEffect(() => {
        const getAccounts = async () => {
            const updatedAccounts = await fetchAccounts(userId)
            setAccounts(updatedAccounts)
            setCurAccount(updatedAccounts[0])
        }
        getAccounts()
    }, [])

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

    return (
        <div className="chart">
            <div className="chart-title">Incomes in Last 10 days</div>
            <ResponsiveContainer width="100%" height={200} className="chart-body">
                <LineChart data={rawChartData}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip
                        contentStyle={{ backgroundColor: "#333333", borderColor: "#F9D342", color: "#F4F4F4" }}
                    />
                    <Legend />
                    <Line type="monotone" dataKey="income" stroke="#F9D342" />
                </LineChart>
            </ResponsiveContainer>
            {isEditMode &&
                <form className="change-chart-type-form">
                    {curAccount && <select value={curAccount.displayName} name="change-chart" onChange={(e) => handleChangeType(e)}>
                        {accounts && accounts.map(acc => (
                            <option name={acc.displayName} key={acc.displayName} value={acc.displayName} >{acc.displayName}</option>
                        ))}
                    </select>}
                </form>
            }
        </div>
    )
}