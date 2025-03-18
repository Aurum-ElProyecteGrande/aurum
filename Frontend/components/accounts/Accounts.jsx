import React, { useEffect, useState } from 'react'
import TransactionSidebar from '../transactions/transaction_sidebar/TransactionSidebar'
import AccountBalances from '../dashboard/charts/account-balances'
import AccountTable from './AccountTable'
import { fetchAccounts, fetchBalance } from '@/scripts/dashboard_scripts/dashboard_scripts'
import { fetchAllCurrency } from '@/scripts/landing_page_scripts/landing_page'
import { fetchPostAccount } from '@/scripts/account_scripts/account-scripts'
import AddForm from './AddForm'
import InfoToast from '../toasts/info-toast'

function Accounts() {

    const [accounts, setAccounts] = useState([])
    const [accountsWithBalance, setAccountsWithBalance] = useState([])
    const [isLoading, setIsLoading] = useState(false)
    const [isAdd, setIsAdd] = useState(false)
    const [addAccName, setAddAccName] = useState("")
    const [addAccAmount, setAddAccAmount] = useState(0)
    const [addAccCurrency, setAddAccCurrency] = useState({})
    const [currencies, setCurrencies] = useState([])

    //TOAST
    const [isToast, setIsToast] = useState(false)
    const [toastText, setToastText] = useState("")
    const [toastType, setToastType] = useState("") //success / fail / null
    const useInfoToast = (text, type) => {
        setToastType(type)
        setToastText(text)
        setIsToast(true)
        setTimeout(() => setIsToast(false), 5000);
    }


    useEffect(() => {
        const getAccounts = async () => {
//            setIsLoading(true)
            const accs = await fetchAccounts()
            setAccounts(accs)
  //          setIsLoading(false)
        }
        getAccounts()
    }, [])

    useEffect(() => {
        const getCurrencies = async () => {
            const currencies = await fetchAllCurrency()
            setCurrencies(currencies)
            setAddAccCurrency(currencies[0])
        }
        getCurrencies()
    }, [])

    useEffect(() => {
        const getBalances = async () => {
      //      setIsLoading(true)
            let updatedAccountsWithBalance = await Promise.all(accounts.map(async (acc) => (
                {
                    accountId: acc.accountId, displayName: acc.displayName, balance: await fetchBalance(acc.accountId), currency: acc.currency, amount: acc.amount
                }))
            )
            setAccountsWithBalance(updatedAccountsWithBalance)
        //    setIsLoading(false)
        }
        if (accounts[0]) {
            getBalances()
        }
    }, [accounts])


    const handleCurrencyChange = (currencyName) => {
        const curCurrency = currencies.find(c => c.name === currencyName)
        setAddAccCurrency(curCurrency)
    }

    const handleAddAccount = async (e) => {
        e.preventDefault()
        const accountDto = {
            displayName: addAccName,
            amount: addAccAmount,
            currencyId: addAccCurrency.currencyId
        }
        const addedAccId = await await fetchPostAccount(accountDto)
        if (addedAccId) {
            let updatedAccounts = [...accounts]
            const addedAcc = {
                accountId: addedAccId,
                displayName: addAccName,
                currency: addAccCurrency,
                amount: addAccAmount
            }
            updatedAccounts.push(addedAcc)
            setAccounts(updatedAccounts)
            setIsAdd(false)
            useInfoToast(`${addAccName} added successfuly`, "success")
        }
    }



    return (
        <section className='accounts'>
            <TransactionSidebar />
            <div className='accounts-header'></div>
            {isLoading ?
                <div className='loader'></div>
                :
                <div className='accounts-container wrapper'>
                    <AccountTable useInfoToast={useInfoToast} accountsWithBalance={accountsWithBalance} setAccounts={setAccounts} accounts={accounts} />
                    {isAdd ?
                        <>
                            <AddForm handleAddAccount={handleAddAccount} setIsAdd={setIsAdd} currencies={currencies} addAccCurrency={addAccCurrency} handleCurrencyChange={handleCurrencyChange} setAddAccAmount={setAddAccAmount} addAccAmount={addAccAmount} setAddAccName={setAddAccName} addAccName={addAccName} />
                        </>
                        :
                        <button className='primary-button add-acc' onClick={() => setIsAdd(true)}>Add new</button>
                    }
                </div>
            }
            <InfoToast toastText={toastText} isToast={isToast} setIsToast={setIsToast} toastType={toastType} />
        </section>
    )
}

export default Accounts