import { displayCurrency, fetchPostIncome } from '@/scripts/dashboard_scripts/dashboard_scripts'
import React, { useEffect, useState } from 'react'


function IncomeForm({ formProps }) {
    const { accounts, incomeCategories, useInfoToast } = formProps

    const [chosenAccount, setChosenAccount] = useState("")
    const [chosenCategory, setChosenCategory] = useState("")
    const [label, setLabel] = useState("")
    const [amount, setAmount] = useState(0)

    const handleAccChange = (accName) => {
        const updatedChosenAccount = accounts.find(a => a.displayName === accName)
        setChosenAccount(updatedChosenAccount)
    }

    const handleCatChange = (catName) => {
        const updatedChosenCategory = incomeCategories.find(i => i.name === catName)
        setChosenCategory(updatedChosenCategory)
    }

    const handleSubmit = async () => {
        const incomeDto = {
            accountId: chosenAccount.accountId,
            categoryId: chosenCategory.categoryId,
            label,
            amount,
            date: new Date().toISOString()
        }

        console.log(incomeDto)

        if (!checkIncomeDtoValidity(incomeDto)) return

        const isSucces = await fetchPostIncome(incomeDto)

        if (isSucces) {
            useInfoToast("Income added successfully!", "success")
        } else {
            useInfoToast("Adding income failed", "fail")
        }
    }

    const checkIncomeDtoValidity = (incomeDto) => {
        if (!incomeDto.accountId ||
            !incomeDto.categoryId ||
            !incomeDto.label ||
            !incomeDto.amount ||
            !incomeDto.date
        ) {
            useInfoToast("Missing income details.", "fail")
            return false
        }
    }

    return (
        <>
            <form className='income-form'>
                <div className='acc dropdown'>
                    <select id="acc" value={chosenAccount} onChange={(e) => handleAccChange(e.target.value)}>
                        <option disabled value="">Select an account</option>
                        {accounts && accounts.map(acc => (
                            <option name={acc.displayName} key={acc.displayName} value={acc.displayName} >{acc.displayName}</option>
                        ))}
                    </select>
                </div>
                <div className='cat dropdown'>
                    <select id="cat" value={chosenCategory} onChange={(e) => handleCatChange(e.target.value)}>
                        <option disabled value="">Select a category</option>
                        {incomeCategories && incomeCategories.map(cat => (
                            <option name={cat.name} key={cat.name} value={cat.name} >{cat.name}</option>
                        ))}
                    </select>
                </div>
                <div className='label input'>
                    <input id="label" type='text' placeholder='Label' onChange={(e) => setLabel(e.target.value)} />
                </div>
                <div className='amount input'>
                    <input id="amount" type="number" placeholder='Amount' onChange={(e) => setAmount(e.target.value)} />
                    <label className='currency' htmlFor='amount'>{chosenAccount.currency && chosenAccount.currency.currencyCode}</label>
                </div>
            </form>
            <button className="submit income-side" onClick={() => handleSubmit()}><span>Submit</span></button>
        </>
    )
}

export default IncomeForm
