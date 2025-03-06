import { useEffect, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';
import ChangeChartForm from '../change-chart-form';
import { fetchCurrencyExchanges, shortenTitle, convertExchangeRate } from '@/scripts/dashboard_scripts/dashboard_scripts';
import ChangeDaysShown from './chart-utils/change-days-shown';

const COLORS = ['#3D62A4', '#F9D342', '#5E946A ', '#C56A64'];

export default function IncomesByCategory({ isEditMode, incomes, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts, accounts, chartLoaded }) {

    const chartName = "income-by-category"

    const [incomesByCategory, setIncomesByCategory] = useState([])
    const [filteredIncomesByCategory, setFilteredIncomesByCategory] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [daysShown, setDaysShown] = useState(30)
    const [currencies, setCurrencies] = useState([])
    const [exchangeRates, setExchangeRates] = useState([])
    const maxShownCategory = 4


    useEffect(() => {
        let allCurrencies = new Set()
        accounts.forEach(acc => {
            allCurrencies.add(acc.currency.currencyCode)
        })
        setCurrencies([...allCurrencies])
    }, [accounts])


    useEffect(() => {
        const getExchangeRates = async () => {
            const updatedExchangeRates = await fetchCurrencyExchanges("HUF", currencies)
            setExchangeRates(updatedExchangeRates)
            chartLoaded(chartName)
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
        const getIncomesByCategory = async () => {

            let updatedIncomes = [...incomes].filter(i => new Date(i.date) >= startDate && new Date(i.date) <= today)

            if (updatedIncomes[0]) {
                updatedIncomes = updatedIncomes.map(i => (
                    { ...i, amount: convertExchangeRate(i.amount, exchangeRates.data, i.currency.currencyCode) }
                ))
            }

            updatedIncomes = updatedIncomes.map(i => {
                return { amount: i.amount, category: i.category.name }
            })

            let rowIncomesByCategory = Object.groupBy(updatedIncomes, ({ category }) => category)
            let updatedIncomesByCategory = []

            for (const category in rowIncomesByCategory) {
                updatedIncomesByCategory.push(
                    rowIncomesByCategory[category].reduce((a, c) => {
                        a.categorySum += c.amount
                        return a
                    }, { category, categorySum: 0 })
                )
            }

            setIncomesByCategory(updatedIncomesByCategory)
        }
        if (incomes[0] && exchangeRates.data) {
            getIncomesByCategory()
        }

    }, [incomes, startDate, exchangeRates])


    useEffect(() => {
        if (incomesByCategory) {
            let updatedFiltered = []
            let sortedIncomesByCategory = incomesByCategory.sort((a, b) => b.categorySum - a.categorySum)
            for (let i = 0; i < maxShownCategory; i++) {
                updatedFiltered.push(sortedIncomesByCategory[i])
            }

            if (updatedFiltered[0]) {
                updatedFiltered = updatedFiltered.map(e => {
                    return {
                        category: shortenTitle(e.category, 13),
                        categorySum: e.categorySum
                    }
                })
            }

            setFilteredIncomesByCategory(updatedFiltered)
        }
    }, [incomesByCategory])

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
                    <p>Incomes by category in last <span className='highlight'>{daysShown}</span> days</p>
                </div>
            </div>

            <div className="chart">

                <div className='expense-by-category-container'>
                    <ResponsiveContainer width="30%" height="100%" className="expense-by-category">
                        <PieChart width={10} height={40}>
                            <Pie
                                data={filteredIncomesByCategory}
                                cx="50%"
                                cy="45%"
                                outerRadius={35}
                                dataKey="categorySum"
                            >
                                {filteredIncomesByCategory.map((entry, index) => (
                                    <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                                ))}
                            </Pie>
                        </PieChart>
                    </ResponsiveContainer>
                    <div className='pie-chart-tooltip'>
                        {filteredIncomesByCategory[0] && filteredIncomesByCategory.map((ec, i) => (
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
