import Chart1 from "../../app/components/dashboard/charts/chart1";
import Chart2 from "../../app/components/dashboard/charts/chart2";
import Chart3 from "../../app/components/dashboard/charts/chart3";
import Chart4 from "../../app/components/dashboard/charts/chart4";
import Chart5 from "../../app/components/dashboard/charts/chart5";
import Chart6 from "../../app/components/dashboard/charts/chart6";
import Chart7 from "../../app/components/dashboard/charts/chart7";

const charts2x1 = [
    { name: "chart1", chart: <Chart1 /> },
    { name: "chart2", chart: <Chart2 /> },
    { name: "chart3", chart: <Chart3 /> }
]
const charts3x3 = [
    { name: "chart4", chart: <Chart4 /> }
]
const charts3x2 = [
    { name: "chart5", chart: <Chart5 /> },
    { name: "chart6", chart: <Chart6 /> }
]
const charts3x1 = [
    { name: "chart7", chart: <Chart7 /> }
]

export const layouts = {
    "basic": {
        possibleCharts: [
            charts2x1,  //.basic-1
            charts2x1,  //.basic-2
            charts2x1,  //.basic-3
            charts3x3,  //.basic-4
            charts3x2,  //.basic-5
            charts3x1,  //.basic-6
            charts3x2   //.basci-7
        ],
        initialCharts: [
            charts2x1[0],
            charts2x1[1],
            charts2x1[2],
            charts3x3[0],
            charts3x2[0],
            charts3x1[0],
            charts3x2[1]
        ]
    },
    "var2": {
        possibleCharts: [
            charts3x1,  //.var2-1
            charts3x1,  //.var2-2
            charts3x2,  //.var2-3
            charts3x3,  //.var2-4
            charts3x1,  //.var2-5
            charts2x1,  //.var2-6
            charts2x1,  //.var2-7
            charts2x1,  //.var2-8
        ],
        initialCharts: [
            charts3x1[0],
            charts3x1[0],
            charts3x2[0],
            charts3x3[0],
            charts3x1[0],
            charts2x1[0],
            charts2x1[1],
            charts2x1[2]
        ]
    },
    "detailed": {
        possibleCharts: [
            charts2x1,  //.detailed-1
            charts2x1,  //.detailed-2
            charts2x1,  //.detailed-3
            charts3x3,  //.detailed-4
            charts3x1,  //.detailed-5
            charts3x1,  //.detailed-6
            charts3x1,  //.detailed-7
            charts3x1,   //.detailed-8
            charts3x1   //.detailed-9
        ],
        initialCharts: [
            charts2x1[0],
            charts2x1[1],
            charts2x1[2],
            charts3x3[0],
            charts3x1[0],
            charts3x1[0],
            charts3x1[0],
            charts3x1[0],
            charts3x1[0]
        ]
    }
}