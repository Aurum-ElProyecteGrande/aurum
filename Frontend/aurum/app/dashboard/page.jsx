"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../components/dashboard/hamburger-menu";
import Header from "../components/dashboard/header";
import ChangeChartForm from "../components/dashboard/change-chart-form";
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


  return (
    <div className="dashboard page">
      <Header setIsHamburgerOpen={setIsHamburgerOpen} isHamburgerOpen={isHamburgerOpen} />
      {isHamburgerOpen &&
        <HamburgerMenu isEditMode={isEditMode} setIsEditMode={setIsEditMode} choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} />
      }
      <div className="dashboard-container">
        {possibleChartsBySegment.map((possibleCharts, segmentIndex) => (
          <div key={`${possibleCharts}-${segmentIndex}`} className={`${choosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>
            {choosenCharts[segmentIndex].chart}
            {isEditMode &&
              <ChangeChartForm choosenCharts={choosenCharts} segmentIndex={segmentIndex} possibleCharts={possibleCharts} setChoosenCharts={setChoosenCharts} />
            }
          </div>
        ))}
      </div>
    </div>
  );
}