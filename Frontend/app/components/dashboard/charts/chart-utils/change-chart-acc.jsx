import React from 'react'

function ChangeChartAcc({ handleChangeType, accounts, curAccount }) {
    return (
        <form className="change-chart-type-form">
            {curAccount && <select value={curAccount.displayName} name="change-chart" onChange={(e) => handleChangeType(e)}>
                {accounts && accounts.map(acc => (
                    <option name={acc.displayName} key={acc.displayName} value={acc.displayName} >{acc.displayName}</option>
                ))}
            </select>}
        </form>
    )
}

export default ChangeChartAcc