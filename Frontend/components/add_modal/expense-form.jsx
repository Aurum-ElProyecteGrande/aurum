import React from 'react'
import { useState, useEffect } from 'react'
import InputWithOptions from './form-components/input-with-options'
import { fetchPostExpense } from '@/scripts/dashboard_scripts/dashboard_scripts'

function ExpenseForm({ formProps }) {

    const { accounts, expenseCategories, useInfoToast } = formProps

    const [chosenAccount, setChosenAccount] = useState(null)
    const [chosenCategory, setChosenCategory] = useState(null)
    const [chosenSubCategory, setChosenSubCategory] = useState("")
    const [possibleSubCategories, setPossibleSubCategories] = useState("")
    const [label, setLabel] = useState("")
    const [amount, setAmount] = useState(0)


    useEffect(() => {
        if (chosenCategory) {
            setPossibleSubCategories(chosenCategory.subCategories)
        }
    }, [chosenCategory])

    const handleAccChange = (accName) => {
        const updatedChosenAccount = accounts.find(a => a.displayName === accName)
        setChosenAccount(updatedChosenAccount)
    }

    const handleCatChange = (catName) => {
        const updatedChosenCategory = expenseCategories.find(e => e.name === catName)
        setChosenCategory(updatedChosenCategory)
    }

    const handleSubmit = async () => {
        const expenseDto = {
            accountId: chosenAccount.accountId,
            categoryId: chosenCategory.categoryId,
            subCategoryName: chosenSubCategory.name ?? null,
            label,
            amount,
            date: new Date().toISOString()
        }

        console.log(expenseDto)
        if (!checkExpenseDtoValidity(expenseDto)) return
        const isSucces = await fetchPostExpense(expenseDto)


        if (isSucces) {
            useInfoToast("Expense added successfully!", "success")
        } else {
            useInfoToast("Adding expense failed", "fail")
        }
    }

    const checkExpenseDtoValidity = (expenseDto) => {
        if (!expenseDto.accountId ||
            !expenseDto.categoryId ||
            !expenseDto.label ||
            !expenseDto.amount ||
            !expenseDto.date
        ) {
            useInfoToast("Missing expense details.", "fail")
            return false
        }
        return true
    }


    return (
        <>
            <form className='expense-form'>
                <div className='acc dropdown'>
                    <select id="acc" value={chosenAccount ? chosenAccount.displayName : ""} onChange={(e) => handleAccChange(e.target.value)}>
                        <option disabled value="">Select an account</option>
                        {accounts && accounts.map(acc => (
                            <option name={acc.displayName} key={acc.displayName} value={acc.displayName} >{acc.displayName}</option>
                        ))}
                    </select>
                </div>
                <div className='cat dropdown'>
                    <select id="cat" value={chosenCategory ? chosenCategory.name : ""} onChange={(e) => handleCatChange(e.target.value)}>
                        <option disabled value="">Select a category</option>
                        {expenseCategories && expenseCategories.map(cat => (
                            <option name={cat.name} key={cat.name} value={cat.name} >{cat.name}</option>
                        ))}
                    </select>
                </div>
                <InputWithOptions id="sub-cat" options={possibleSubCategories} inputValue={chosenSubCategory} setInputValue={setChosenSubCategory} />
                <div className='label input'>
                    <input id="label" type='text' placeholder='Label' onChange={(e) => setLabel(e.target.value)} />
                </div>
                <div className='amount input'>
                    <input id="amount" type="number" step="0.01" placeholder='Amount' onChange={(e) => setAmount(e.target.value)} />
                    <label className='currency' htmlFor='amount'>{chosenAccount?.currency && chosenAccount.currency.currencyCode}</label>
                </div>
            </form>
            <button className='submit expense-side' onClick={() => handleSubmit()}><span>Submit</span></button>
        </>
    )
}


export default ExpenseForm
