import { useEffect, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';
import ChangeChartForm from '../change-chart-form';
import { shortenTitle, fetchCurrencyExchanges, convertExchangeRate } from '@/scripts/dashboard_scripts/dashboard_scripts';
import ChangeDaysShown from './chart-utils/change-days-shown';

const COLORS = ['#3D62A4', '#F9D342', '#5E946A ', '#C56A64'];

export default function ExpenseByCategory({ isEditMode, expenses, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts, accounts }) {

    const [expensesByCategory, setExpensesByCategory] = useState([])
    const [filteredExpensesByCategory, setFilteredExpensesByCategory] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [daysShown, setDaysShown] = useState(30)
    const [currencies, setCurrencies] = useState([])
    const [exchangeRates, setExchangeRates] = useState([])
    const maxShownCategory = 4


    useEffect(() => {
        if (accounts[0]) {
            let allCurrencies = new Set()

            accounts.forEach(acc => {
                allCurrencies.add(acc.currency.currencyCode)
            })

            setCurrencies([...allCurrencies])
        }
    }, [accounts])

    useEffect(() => {
        const getExchangeRates = async () => {
            const updatedExchangeRates = await fetchCurrencyExchanges("HUF", currencies)
            setExchangeRates(updatedExchangeRates)
        }
        if (currencies[0]) {
            getExchangeRates()
        }
    }, [currencies])

    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(today.getDate() - daysShown);
        setStartDate(nDaysAgo)
    }, [daysShown])


    useEffect(() => {
        const getExpensesByCategory = async () => {

            let updatedExpenses = [...expenses].filter(e => new Date(e.date) >= startDate && new Date(e.date) <= today)

            if (updatedExpenses[0]) {
                updatedExpenses = updatedExpenses.map(e => (
                    { ...e, amount: convertExchangeRate(e.amount, exchangeRates.data, e.currency.currencyCode) }
                ))
            }

            updatedExpenses = updatedExpenses.map(e => {
                return { amount: e.amount, category: e.category.name }
            })

            let rowExpensesByCategory = Object.groupBy(updatedExpenses, ({ category }) => category)
            let updatedExpensesByCategory = []

            for (const category in rowExpensesByCategory) {
                updatedExpensesByCategory.push(
                    rowExpensesByCategory[category].reduce((a, c) => {
                        a.categorySum += c.amount
                        return a
                    }, { category, categorySum: 0 })
                )
            }

            setExpensesByCategory(updatedExpensesByCategory)
        }
        if (expenses[0] && exchangeRates.data) {
            getExpensesByCategory()
        }
    }, [expenses, startDate, exchangeRates])


    useEffect(() => {
        if (expensesByCategory) {
            let updatedFiltered = []
            let sortedExpensesByCategory = expensesByCategory.sort((a, b) => b.categorySum - a.categorySum)
            for (let i = 0; i < maxShownCategory; i++) {
                updatedFiltered.push(sortedExpensesByCategory[i])
            }

            if (updatedFiltered[0]) {
                updatedFiltered = updatedFiltered.map(e => {
                    return {
                        category: shortenTitle(e.category, 13),
                        categorySum: e.categorySum
                    }
                })
            }

            setFilteredExpensesByCategory(updatedFiltered)
        }
    }, [expensesByCategory])


    const handleChangeDays = (e) => {
        setDaysShown(e.target.value)
    }


    return (
        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>

            <div className='chart-title-container'>
                {isEditMode &&
                    <div className="change-chart-types-container">
                        <ChangeDaysShown handleChangeDays={handleChangeDays} daysShown={daysShown} />
                        <ChangeChartForm
                            choosenCharts={choosenCharts}
                            segmentIndex={segmentIndex}
                            possibleCharts={possibleChartsBySegment[segmentIndex]}
                            setChoosenCharts={setChoosenCharts} />
                    </div>
                }
                <div className="chart-title">
                    <p>Expenses by category in last <span className='highlight'>{daysShown}</span> days</p>
                </div>
            </div>

            <div className="chart">

                <div className='expense-by-category-container'>
                    <ResponsiveContainer width="30%" height="100%" className="expense-by-category">
                        <PieChart width={10} height={40}>
                            <Pie
                                data={filteredExpensesByCategory}
                                cx="50%"
                                cy="45%"
                                outerRadius={35}
                                dataKey="categorySum"
                            >
                                {filteredExpensesByCategory.map((entry, index) => (
                                    <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                                ))}
                            </Pie>
                        </PieChart>
                    </ResponsiveContainer>
                    <div className='pie-chart-tooltip'>
                        {filteredExpensesByCategory[0] && filteredExpensesByCategory.map((ec, i) => (
                            <div key={ec.category} className='row' style={{ borderBottom: `1px solid ${COLORS[i % COLORS.length]}` }}>
                                <div className='label' style={{ color: COLORS[i % COLORS.length] }}>{ec.category}</div>
                                <div className='value'>{ec.categorySum.toLocaleString('hu-HU', { style: 'currency', currency: 'HUF' })}</div>
                            </div>
                        ))}
                    </div>
                </div>
            </div >

        </div>
    );

}
