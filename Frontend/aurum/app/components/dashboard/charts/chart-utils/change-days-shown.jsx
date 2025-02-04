import React from 'react'

function ChangeDaysShown({ handleChangeDays, daysShown }) {
    const possibleDaysShown = [5, 10, 15, 20, 25, 30]
    return (
        <form className="change-chart-type-form days-shown-form">
            {daysShown && <select value={daysShown} name="change-chart" onChange={(e) => handleChangeDays(e)}>
                {possibleDaysShown.map(days => (
                    <option name={days} key={days} value={days} >{days}</option>
                ))}
            </select>}
        </form>)
}

export default ChangeDaysShown