import React from 'react'
import { useState, useEffect } from 'react'
import InputWithOptions from './form-components/input-with-options'
import { fetchPostExpense, fetchPostRegularExpense } from '@/scripts/dashboard_scripts/dashboard_scripts'
import RegularitySelector from './regularity-selector'

function ExpenseForm({ formProps }) {

    const { accounts, expenseCategories, useInfoToast } = formProps

    const [chosenAccount, setChosenAccount] = useState(null)
    const [chosenCategory, setChosenCategory] = useState(null)
    const [chosenSubCategory, setChosenSubCategory] = useState("")
    const [possibleSubCategories, setPossibleSubCategories] = useState("")
    const [label, setLabel] = useState("")
    const [amount, setAmount] = useState(0)
    const [regularity, setRegularity] = useState("None")


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
            subCategoryName: chosenSubCategory ?? null,
            label,
            amount
        }

        if (regularity != "None") {
            expenseDto.startDate = new Date().toISOString()
            expenseDto.regularity = regularity
        }
        else
            expenseDto.date = new Date().toISOString()


        console.log(expenseDto)
        if (!checkExpenseDtoValidity(expenseDto)) return
        const isSucces = regularity == "None" ?
            await fetchPostExpense(expenseDto) :
            await fetchPostRegularExpense(expenseDto)


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
            !(expenseDto.date ||
                (expenseDto.startDate && expenseDto.regularity))
        ) {
            useInfoToast("Missing expense details.", "fail")
            return false
        }
        return true
    }

    const onSelect = (value) => {
        setRegularity(value)
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
                <RegularitySelector onSelect={onSelect} />
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
