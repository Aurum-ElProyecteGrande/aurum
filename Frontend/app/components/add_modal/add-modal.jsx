import React, { useEffect, useState } from 'react'
import IncomeForm from './income-form'
import ExpenseForm from './expense-form'
import { fetchExpenseCategories, fetchIncomeCategories } from '@/scripts/dashboard_scripts/dashboard_scripts'
import { GiReceiveMoney, GiPayMoney } from "react-icons/gi";
import { ImCross } from "react-icons/im";

function AddModal({ accounts, useInfoToast, setIsAddModal }) {

    const [isIncome, setIsIncome] = useState(true)
    const [incomeCategories, setIncomeCategories] = useState([])
    const [expenseCategories, setExpenseCategories] = useState([])

    const formProps = { accounts, incomeCategories, expenseCategories, useInfoToast }

    useEffect(() => {
        const getCategories = async () => {
            const updatedIncomeCategories = await fetchIncomeCategories()
            const updatedExpenseCategories = await fetchExpenseCategories()
            setIncomeCategories(updatedIncomeCategories)
            setExpenseCategories(updatedExpenseCategories)
        }
        getCategories()
    }, [])

    return (
        <div className='add-modal-background'>
            <div className='modal'>
                <button className={`income ${isIncome && "active"}`} onClick={() => setIsIncome(true)}>
                    {isIncome &&
                        <span>Income</span>
                    }
                    <GiReceiveMoney className='icon' />
                </button>
                <button className={`expense ${!isIncome && "active"}`} onClick={() => setIsIncome(false)}>
                    <GiPayMoney className='icon' />
                    {!isIncome &&
                        <span>Expense</span>
                    }
                </button>
                {isIncome ?
                    <IncomeForm formProps={formProps} />
                    :
                    <ExpenseForm formProps={formProps} />
                }
                <ImCross className={`close ${!isIncome && "blue"}`} onClick={() => setIsAddModal(false)} />
            </div>
        </div>
    )
}

export default AddModal