import ChangeChartForm from '../change-chart-form';

export default function TemplateChart({ isEditMode, segmentIndex, chosenLayout, choosenCharts ,possibleChartsBySegment, setChoosenCharts}) {

    return (
        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>
            <div className="chart">
                <div className="chart-title">
                    <p>TemplateChart</p>
                </div>
                <div className="chart-body">

                </div>
            </div>
            {isEditMode &&
                <div className="change-chart-types-container">
                    <ChangeChartForm
                        choosenCharts={choosenCharts}
                        segmentIndex={segmentIndex}
                        possibleCharts={possibleChartsBySegment[segmentIndex]}
                        setChoosenCharts={setChoosenCharts} />
                </div>
            }
        </div>
    )
}
