import ExpenseLineChart from "../../app/components/dashboard/charts/expense-line-chart";
import IncomeLineChart from "../../app/components/dashboard/charts/income-line-chart";
import AccountBallances from "../../app/components/dashboard/charts/account-ballances";
import LastTransactions from "../../app/components/dashboard/charts/last-transactions";
import Chart5 from "../../app/components/dashboard/charts/chart5";
import Chart6 from "../../app/components/dashboard/charts/chart6";
import Chart7 from "../../app/components/dashboard/charts/chart7";

const charts2x1 = [
    { name: "chart5", chart: <Chart5 /> },
    { name: "chart5", chart: <Chart5 /> },
    { name: "account-ballances", chart: <AccountBallances /> }
]
const charts3x3 = [
    { name: "last-transactions", chart: <LastTransactions /> }
]
const charts3x2 = [
    { name: "expense-line-chart", chart: <ExpenseLineChart /> },
    { name: "income-line-chart", chart: <IncomeLineChart /> },
    { name: "chart6", chart: <Chart6 /> }
]
const charts3x1 = [
    { name: "chart7", chart: <Chart7 /> }
]

export const allCharts = [
    charts2x1,
    charts3x3,
    charts3x2,
    charts3x1
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
    "scientic": {
        possibleCharts: [
            charts3x1,  //.scientic-1
            charts3x1,  //.scientic-2
            charts3x2,  //.scientic-3
            charts3x3,  //.scientic-4
            charts3x1,  //.scientic-5
            charts2x1,  //.scientic-6
            charts2x1,  //.scientic-7
            charts2x1,  //.scientic-8
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