import { useEffect, useState } from 'react';
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';
import { fetchExpensesByDate } from '@/scripts/dashboard_scripts/dashboard_scripts';

const data = [
    { name: 'Group A', value: 400 },
    { name: 'Group B', value: 300 },
    { name: 'Group C', value: 300 },
    { name: 'Group D', value: 200 },
];

const COLORS = ['#3D62A4', '#F9D342', '#5E946A ', '#C56A64'];

export default function ExpenseByCategory({ isEditMode, accounts }) {

    const [expensesByCategory, setExpensesByCategory] = useState([])
    const [filteredExpensesByCategory, setFilteredExpensesByCategory] = useState([])
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
        const getExpensesByCategory = async () => {
            let updatedExpenses = []
            await Promise.all(accounts.map(async (acc) => {
                updatedExpenses.push(...await fetchExpensesByDate(acc.accountId, startDate.toISOString().slice(0, 10), today.toISOString().slice(0, 10)))
            }))

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
        if (accounts) {
            getExpensesByCategory()
        }
    }, [accounts])


    useEffect(() => {
        if (expensesByCategory) {
            let updatedFiltered = []
            let sortedExpensesByCategory = expensesByCategory.sort((a, b) => b.categorySum - a.categorySum)
            for (let i = 0; i < maxShownCategory; i++) {
                updatedFiltered.push(sortedExpensesByCategory[i])
            }
            setFilteredExpensesByCategory(updatedFiltered)
        }
    }, [expensesByCategory])

    console.log(filteredExpensesByCategory)

    return (
        <div className="chart">
            <div className="chart-title">
                <p>Expenses by category in last {daysCalculated} days</p>
            </div>
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
            {
                isEditMode &&
                <div className="change-chart-types-container">
                </div>
            }
        </div >
    );

}
