import React, { useState } from 'react'
import { displayCurrency } from '@/scripts/dashboard_scripts/dashboard_scripts'
import { MdEdit, MdDelete } from "react-icons/md";
import { TiTick } from "react-icons/ti";
import { ImCross } from "react-icons/im";
import { fetchUpdateAccount, fetchDeleteAccount } from '@/scripts/account_scripts/account-scripts';



function AccountTable({ accountsWithBalance, setAccounts, accounts, useInfoToast }) {

    const [editId, setEditId] = useState(0)
    const [accountName, setAccountName] = useState("")
    const [hoveredId, setHoveredId] = useState(0)

    const handleEditClick = (acc) => {
        setAccountName(acc.displayName)
        setEditId(acc.accountId)
    }

    const handleSubmitClick = async (acc) => {
        const modifyAccountDto = {
            displayName: accountName,
            amount: acc.amount,
            currencyId: acc.currency.currencyId
        }

        if (await fetchUpdateAccount(acc.accountId, modifyAccountDto)) {
            let updatedAccounts = [...accounts]
            updatedAccounts = updatedAccounts.map(a => a.accountId == acc.accountId ? { ...a, displayName: accountName } : a)
            setAccounts(updatedAccounts)
            setEditId(0)
            useInfoToast("Account updated successfuly", "success")
        }
    }

    const handleDeleteClick = async (accId) => {
        if (await fetchDeleteAccount(accId)) {
            let updatedAccounts = [...accounts]
            updatedAccounts = updatedAccounts.filter(a => a.accountId !== accId)
            setAccounts(updatedAccounts)
            useInfoToast(`Account deleted successfuly`, "success")
        }
    }


    return (
        <table className="accounts-table wrapper">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Balance</th>
                </tr>
            </thead>
            <tbody>
                {accountsWithBalance.map((acc, i) => (
                    editId === acc.accountId ?
                        <tr key={i}>
                            <td>
                                <input type='text' value={accountName} onChange={(e) => setAccountName(e.target.value)}></input>
                            </td>
                            <td>
                                <div className='td-wrapper'>
                                    {displayCurrency(acc.balance, acc.currency.currencyCode)}
                                    <div className='icons'>
                                        <div className='submit'>
                                            <TiTick onClick={() => handleSubmitClick(acc)} />
                                        </div>
                                        <div className='back' onClick={() => setEditId(0)}>
                                            <ImCross />
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        :
                        <tr key={i} onMouseOver={() => setHoveredId(acc.accountId)} onMouseOut={() => setHoveredId(0)}>
                            <td>{acc.displayName}</td>
                            <td>
                                <div className='td-wrapper'>
                                    {displayCurrency(acc.balance, acc.currency.currencyCode)}
                                    {hoveredId == acc.accountId &&
                                        <div className='icons'>
                                            <div className='edit'>
                                                <MdEdit onClick={() => handleEditClick(acc)} />
                                            </div>
                                            {accounts.length > 1 &&
                                                <div className='delete'>
                                                    <MdDelete onClick={() => handleDeleteClick(acc.accountId)} />
                                                </div>
                                            }
                                        </div>
                                    }
                                </div>
                            </td>
                        </tr>
                ))}
            </tbody>
        </table>
    )
}

export default AccountTable