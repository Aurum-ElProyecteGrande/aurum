"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../components/dashboard/hamburger-menu";
import Header from "../components/dashboard/header";
import ChangeChartForm from "../components/dashboard/change-chart-form";
import Sidebar from "../components/sidebar";
import { layouts } from "../../scripts/dashboard_scripts/layouts"
import { fetchAccounts, fetchLayouts, fetchPostLayout } from "@/scripts/dashboard_scripts/dashboard_scripts";
import { getIndexOfPossibleChart } from "@/scripts/dashboard_scripts/dashboard_scripts";

export default function DashboardPage() {

  const [chosenLayout, setChosenLayout] = useState("basic")
  const [possibleChartsBySegment, setPossibleChartsBySegment] = useState(layouts["basic"].possibleCharts)
  const [choosenCharts, setChoosenCharts] = useState()  //layouts["basic"].initialCharts
  const [isEditMode, setIsEditMode] = useState(false)
  const [isHamburgerOpen, setIsHamburgerOpen] = useState(false)
  const [userInitialChartNames, setUserInitialChartNames] = useState()

  //chart states
  const [accounts, setAccounts] = useState([])
  const userId = 1 //from credentials probably? TODO

  //chart effects
  useEffect(() => {
    const getAccounts = async () => {
      const updatedAccounts = await fetchAccounts(userId)
      setAccounts(updatedAccounts)
    }
    getAccounts()
  }, [])
  //\

  // save layout
  const loadLayouts = async () => {
    return setUserInitialChartNames(await fetchLayouts(userId))
  }

  useEffect(() => {
    loadLayouts()
  }, [])
  //\

  //gets saved layout if has in DB else initial layout
  useEffect(() => {
    const updatedPossibleChartsBySegment = layouts[chosenLayout].possibleCharts

    if (userInitialChartNames) {

      if (userInitialChartNames[chosenLayout].length > 0) {
        const userInitialChartIndexes = userInitialChartNames[chosenLayout].map(n => getIndexOfPossibleChart(n))
        let updatedUserInitialCharts = []
        for (let i = 0; i < updatedPossibleChartsBySegment.length; i++) {
          updatedUserInitialCharts.push(updatedPossibleChartsBySegment[i][userInitialChartIndexes[i]])
        }
        setChoosenCharts(updatedUserInitialCharts)
      } else {
        setChoosenCharts(layouts[chosenLayout].initialCharts)
      }

    } else {
      setChoosenCharts(layouts[chosenLayout].initialCharts)
    }

    setPossibleChartsBySegment(updatedPossibleChartsBySegment)

  }, [chosenLayout, userInitialChartNames])

  //save & reload chosen layout
  const saveChoosenChartsForUser = async () => {
    const chosenChartNames = choosenCharts.map(chart => chart.name)
    const layoutDto = {
      userId: userId,
      layoutName: chosenLayout,
      charts: chosenChartNames
    }

    await fetchPostLayout(layoutDto)
    await loadLayouts()

    window.alert("Layout saved!")
  }

  return (
    <div className="dashboard page">
      <Header
        setIsHamburgerOpen={setIsHamburgerOpen}
        isHamburgerOpen={isHamburgerOpen}
        isEditMode={isEditMode}
        chosenLayout={chosenLayout}
        saveChoosenChartsForUser={saveChoosenChartsForUser} />
      <Sidebar />
      {isHamburgerOpen &&
        <HamburgerMenu
          isEditMode={isEditMode}
          setIsEditMode={setIsEditMode}
          chosenLayout={chosenLayout}
          setChosenLayout={setChosenLayout} />
      }
      <div className="dashboard-container">
        {choosenCharts && choosenCharts.map((choosenChart, segmentIndex) => (
          <div key={segmentIndex}
            className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>
            {React.cloneElement(
              choosenChart.chart,
              { isEditMode, accounts })
            }
            {isEditMode &&
              <ChangeChartForm
                choosenCharts={choosenCharts}
                segmentIndex={segmentIndex}
                possibleCharts={possibleChartsBySegment[segmentIndex]}
                setChoosenCharts={setChoosenCharts} />
            }
          </div>
        ))}
      </div>
    </div>
  );
}




/*

  return (
    <div className="dashboard page">
      <Header setIsHamburgerOpen={setIsHamburgerOpen} isHamburgerOpen={isHamburgerOpen} isEditMode={isEditMode} />
      <Sidebar />
      {isHamburgerOpen &&
        <HamburgerMenu isEditMode={isEditMode} setIsEditMode={setIsEditMode} choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} />
      }
      <div className="dashboard-container">
        {possibleChartsBySegment.map((possibleCharts, segmentIndex) => (
          <div key={segmentIndex}
            className={`${choosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>
            {React.cloneElement(choosenCharts[segmentIndex].chart,
              { isEditMode, accounts })}
            {isEditMode &&
              <ChangeChartForm choosenCharts={choosenCharts} segmentIndex={segmentIndex} possibleCharts={possibleCharts} setChoosenCharts={setChoosenCharts} />
            }
          </div>
        ))}
      </div>
    </div>
  );
*/