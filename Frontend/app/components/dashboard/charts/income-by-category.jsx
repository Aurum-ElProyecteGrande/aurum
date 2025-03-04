import { useEffect, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';
import ChangeChartForm from '../change-chart-form';
import { shortenTitle } from '@/scripts/dashboard_scripts/dashboard_scripts';

const COLORS = ['#3D62A4', '#F9D342', '#5E946A ', '#C56A64'];

export default function IncomesByCategory({ isEditMode, incomes, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts }) {

    const [incomesByCategory, setIncomesByCategory] = useState([])
    const [filteredIncomesByCategory, setFilteredIncomesByCategory] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [daysCalculated, setDaysCalculated] = useState(30)
    const maxShownCategory = 4


    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(today.getDate() - daysCalculated);
        setStartDate(nDaysAgo)
    }, [daysCalculated])


    useEffect(() => {
        const getIncomesByCategory = async () => {

            let updatedIncomes = [...incomes].filter(e => new Date(e.date) >= startDate && new Date(e.date) <= today)

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
        if (incomes[0]) {
            getIncomesByCategory()
        }

    }, [incomes, startDate])


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
                    <p>Incomes by category in last {daysCalculated} days</p>
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
