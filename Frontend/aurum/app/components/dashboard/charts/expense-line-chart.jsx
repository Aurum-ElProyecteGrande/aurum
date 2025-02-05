"use client"

import { useEffect, useState } from "react";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from "recharts";
import { fetchExpenses } from "@/scripts/dashboard_scripts/dashboard_scripts";
import ChangeChartType from "./chart-utils/change-chart-type";
import ChangeDaysShown from "./chart-utils/change-days-shown";

export default function ExpenseLineChart({ isEditMode, accounts }) {

    const [expensesByDateString, setExpensesByDateString] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [chartData, setChartData] = useState([])
    const [rawChartData, setRawChartData] = useState([])
    const [curAccount, setCurAccount] = useState()
    const [daysShown, setDaysShown] = useState(10)

    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(today.getDate() - daysShown);
        setStartDate(nDaysAgo)
    }, [daysShown])

    useEffect(() => {
        if (accounts) {
            setCurAccount(accounts[0])
        }
    }, [accounts])

    useEffect(() => {
        const getExpenses = async (accId) => {
            const updatedExpenses = await fetchExpenses(accId)
            let expensesByDate = Object.groupBy(updatedExpenses, ({ date }) => date)
            let updatedExpenseByDateString = []
            for (const key in expensesByDate) {
                updatedExpenseByDateString[key.toString().slice(0, 10)] = expensesByDate[key]
            }
            setExpensesByDateString(updatedExpenseByDateString)
        }
        if (curAccount) {
            getExpenses(curAccount.accountId)
        }
    }, [curAccount])



    useEffect(() => {
        let updatedChartData = []
        for (let i = 0; i <= daysShown; i++) {
            let curDate = new Date(startDate)
            curDate.setDate(startDate.getDate() + i)
            let dateString = curDate.toISOString().slice(0, 10)

            if (expensesByDateString[dateString]) {
                let sum = 0
                for (const expense of expensesByDateString[dateString]) {
                    sum += expense.amount
                }
                updatedChartData[dateString] = {
                    name: dateString,
                    expense: sum
                }
            } else {
                updatedChartData[dateString] = {
                    name: dateString,
                    expense: 0
                }
            }
        }
        setChartData(updatedChartData)
    }, [startDate, expensesByDateString])


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

    return (
        <div className="chart">
            <div className="chart-title">Expenses in Last 10 days</div>
            <ResponsiveContainer width="100%" height={200} className="chart-body">
                <LineChart data={rawChartData}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip
                        contentStyle={{ backgroundColor: "#333333", borderColor: "#F9D342", color: "#F4F4F4" }}
                    />
                    <Legend />
                    <Line type="monotone" dataKey="expense" stroke="#F9D342" name={curAccount && curAccount.displayName} />
                </LineChart>
            </ResponsiveContainer>
            {isEditMode &&
                <div className="change-chart-types-container">
                    <ChangeChartType handleChangeType={handleChangeType} accounts={accounts} curAccount={curAccount} />
                    <ChangeDaysShown handleChangeDays={handleChangeDays} daysShown={daysShown} />
                </div>
            }
        </div>
    )
}