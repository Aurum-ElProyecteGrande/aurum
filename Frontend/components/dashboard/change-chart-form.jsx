export default function ChangeChartForm({ chosenCharts, segmentIndex, possibleCharts, setChosenCharts }) {

    const handleChangeChartClick = (event, segmentIndex, possibleCharts) => {
        let updatedChosenCharts = [...chosenCharts]
        let chartName = event.target.value
        updatedChosenCharts[segmentIndex] = possibleCharts.find(c => c.name === chartName)
        setChosenCharts(updatedChosenCharts)
    }

    return (
        <form className="change-chart-form">
            <select value={chosenCharts[segmentIndex].name} name="change-chart" onChange={(e) => handleChangeChartClick(e, segmentIndex, possibleCharts)}>
                {possibleCharts.map(chart => (
                    <option name={chart.name} key={chart.name} value={chart.name} >{chart.name}</option>
                ))}
            </select>
        </form>
    )
}