export default function ChangeChartForm({ choosenCharts, segmentIndex, possibleCharts, setChoosenCharts }) {

    const handleChangeChartClick = (event, segmentIndex, possibleCharts) => {
        let updatedChoosenCharts = [...choosenCharts]
        let chartName = event.target.value
        updatedChoosenCharts[segmentIndex] = possibleCharts.find(c => c.name === chartName)
        setChoosenCharts(updatedChoosenCharts)
    }

    return (
        <form className="change-chart-form">
            <select value={choosenCharts[segmentIndex].name} name="change-chart" onChange={(e) => handleChangeChartClick(e, segmentIndex, possibleCharts)}>
                {possibleCharts.map(chart => (
                    <option name={chart.name} key={chart.name} value={chart.name} >{chart.name}</option>
                ))}
            </select>
        </form>
    )
}