import { useEffect, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';
import { fetchIncomeByDate } from '@/scripts/dashboard_scripts/dashboard_scripts';

const COLORS = ['#3D62A4', '#F9D342', '#5E946A ', '#C56A64'];

export default function IncomesByCategory({ isEditMode, accounts, isLoading, setIsLoading }) {

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

            let updatedIncomes = []

            setIsLoading(true)

            await Promise.all(accounts.map(async (acc) => {
                updatedIncomes.push(...await fetchIncomeByDate(acc.accountId, startDate.toISOString().slice(0, 10), today.toISOString().slice(0, 10)))
            }))
            
            setIsLoading(false)

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
        if (accounts[0] && !isLoading) {
            getIncomesByCategory()
        }
    }, [accounts, isLoading])



    useEffect(() => {
        if (incomesByCategory) {
            let updatedFiltered = []
            let sortedIncomesByCategory = incomesByCategory.sort((a, b) => b.categorySum - a.categorySum)
            for (let i = 0; i < maxShownCategory; i++) {
                updatedFiltered.push(sortedIncomesByCategory[i])
            }
            setFilteredIncomesByCategory(updatedFiltered)
        }
    }, [incomesByCategory])

    return (
        <div className="chart">
            <div className="chart-title">
                <p>Incomes by category in last {daysCalculated} days</p>
            </div>
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
            {
                isEditMode &&
                <div className="change-chart-types-container">
                </div>
            }
        </div >
    );

}
