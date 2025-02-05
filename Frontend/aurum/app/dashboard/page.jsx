"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../components/dashboard/hamburger-menu";
import Header from "../components/dashboard/header";
import ChangeChartForm from "../components/dashboard/change-chart-form";
import Sidebar from "../components/sidebar";
import { layouts } from "../../scripts/dashboard_scripts/layouts"
import { fetchAccounts, fetchExpenses } from "@/scripts/dashboard_scripts/dashboard_scripts";

export default function DashboardPage() {

  const [choosenLayout, setChoosenLayout] = useState("basic")
  const [isEditMode, setIsEditMode] = useState(false)
  const [isHamburgerOpen, setIsHamburgerOpen] = useState(false)
  const [possibleChartsBySegment, setPossibleChartsBySegment] = useState(layouts["basic"].possibleCharts)
  const [choosenCharts, setChoosenCharts] = useState(layouts["basic"].initialCharts)

  //chart states
  const [accounts, setAccounts] = useState([])
  const userId = 1 //FROM CREDENTIALS probably?

  //chart effects
  useEffect(() => {
    const getAccounts = async () => {
      const updatedAccounts = await fetchAccounts(userId)
      setAccounts(updatedAccounts)
    }
    getAccounts()
  }, [])

  //\

  useEffect(() => {
    setPossibleChartsBySegment(layouts[choosenLayout].possibleCharts)
    setChoosenCharts(layouts[choosenLayout].initialCharts)
  }, [choosenLayout])


  return (
    <div className="dashboard page">
      <Header setIsHamburgerOpen={setIsHamburgerOpen} isHamburgerOpen={isHamburgerOpen} isEditMode={isEditMode} />
      <Sidebar />
      {isHamburgerOpen &&
        <HamburgerMenu isEditMode={isEditMode} setIsEditMode={setIsEditMode} choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} />
      }
      <div className="dashboard-container">
        {possibleChartsBySegment.map((possibleCharts, segmentIndex) => (
          <div key={segmentIndex} className={`${choosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>
            {React.cloneElement(choosenCharts[segmentIndex].chart, { isEditMode, accounts })}
            {isEditMode &&
              <ChangeChartForm choosenCharts={choosenCharts} segmentIndex={segmentIndex} possibleCharts={possibleCharts} setChoosenCharts={setChoosenCharts} />
            }
          </div>
        ))}
      </div>
    </div>
  );
}