import React from 'react'
import { ImCross } from "react-icons/im";

function AddForm({ handleCurrencyChange, setAddAccName, setAddAccAmount, addAccName, addAccAmount, addAccCurrency, currencies, setIsAdd, handleAddAccount}) {
    return (
        <form>
            <input
                type="text"
                name="accountName"
                placeholder="Account Name"
                value={addAccName}
                onChange={(e) => setAddAccName(e.target.value)}
                required
            />
            <input
                type="number"
                name="accountAmount"
                placeholder="Initial Amount"
                value={addAccAmount}
                onChange={(e) => setAddAccAmount(e.target.value)}
                required
            />
            <select id="currency" value={addAccCurrency ? addAccCurrency.name : ""} onChange={(e) => handleCurrencyChange(e.target.value)}>
                {currencies && currencies.map(c => (
                    <option name={c.name} key={c.name} value={c.name} >{c.name}</option>
                ))}
            </select>
            <div className='buttons'>
                <button onClick={(e) => handleAddAccount(e)}>Add</button>
                <ImCross className='close-add' onClick={() => setIsAdd(false)} />
            </div>

        </form>
    )
}

export default AddForm