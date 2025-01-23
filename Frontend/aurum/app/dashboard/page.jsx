"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../components/dashboard/hamburger-menu";
import Header from "../components/dashboard/header";
import { layouts } from "../../scripts/dashboard_scripts/dashboard_scripts"

export default function DashboardPage() {

  const [choosenLayout, setChoosenLayout] = useState("basic")
  const [isEditMode, setIsEditMode] = useState(false)
  const [isHamburgerOpen, setIsHamburgerOpen] = useState(false)
  const [possibleChartsBySegment, setPossibleChartsBySegment] = useState(layouts["basic"].possibleCharts)
  const [choosenCharts, setChoosenCharts] = useState(layouts["basic"].initialCharts)

  useEffect(() => {
    setPossibleChartsBySegment(layouts[choosenLayout].possibleCharts)
    setChoosenCharts(layouts[choosenLayout].initialCharts)
  }, [choosenLayout])


  const handleChangeChartClick = (event, segmentIndex, possibleCharts) => {
    let updatedChoosenCharts = [...choosenCharts]
    let chartName = event.target.value
    updatedChoosenCharts[segmentIndex] = possibleCharts.find(c => c.name === chartName)
    setChoosenCharts(updatedChoosenCharts)
  }

  console.log(choosenCharts)

  return (
    <div className="dashboard page">
      <Header setIsHamburgerOpen={setIsHamburgerOpen} isHamburgerOpen={isHamburgerOpen} />
      {isHamburgerOpen &&
        <HamburgerMenu isEditMode={isEditMode} setIsEditMode={setIsEditMode} choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} />
      }
      <div className="dashboard-container">
        {possibleChartsBySegment.map((possibleCharts, segmentIndex) => (
          <div key={`${possibleCharts}-${segmentIndex}`} className={`${choosenLayout}-${segmentIndex + 1} chart-container`}>
            {choosenCharts[segmentIndex].chart}
            {isEditMode &&
              <form className="change-chart-form">
                <label>change chart</label>
                <select value={choosenCharts[segmentIndex].name} name="change-chart" onChange={(e) => handleChangeChartClick(e, segmentIndex, possibleCharts)}>
                  {possibleCharts.map(chart => (
                    <option name={chart.name} key={chart.name} value={chart.name} >{chart.name}</option>
                  ))}
                </select>
              </form>}
          </div>
        ))}
      </div>
    </div>
  );
}