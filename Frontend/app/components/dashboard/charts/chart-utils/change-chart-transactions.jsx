import React from 'react'

function ChangeChartTransactions({ handleChangeTransactions, nrOfTransactions, curNrOfTransactions }) {
    return (
        <form className="change-chart-type-form">
            {curNrOfTransactions && <select value={curNrOfTransactions} name="change-chart" onChange={(e) => handleChangeTransactions(e)}>
                {nrOfTransactions && nrOfTransactions.map(tr => (
                    <option name={tr} key={tr} value={tr} >{tr}</option>
                ))}
            </select>}
        </form>
    )
}

export default ChangeChartTransactions