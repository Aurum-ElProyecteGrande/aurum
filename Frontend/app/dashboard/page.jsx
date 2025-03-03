"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../components/dashboard/hamburger-menu";
import Header from "../components/dashboard/header";
import ChangeChartForm from "../components/dashboard/change-chart-form";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import Sidebar from "../components/sidebar";
import { layouts } from "../../scripts/dashboard_scripts/layouts"
import { fetchAccounts, fetchExpenses, fetchExpensesWithCurrency, fetchIncome, fetchIncomesWithCurrency, fetchLayouts, fetchPostLayout, fetchUserName } from "@/scripts/dashboard_scripts/dashboard_scripts";
import { getIndexOfPossibleChart } from "@/scripts/dashboard_scripts/dashboard_scripts";

export default function DashboardPage() {

  const [chosenLayout, setChosenLayout] = useState("basic")
  const [possibleChartsBySegment, setPossibleChartsBySegment] = useState(layouts["basic"].possibleCharts)
  const [choosenCharts, setChoosenCharts] = useState()  //layouts["basic"].initialCharts
  const [isEditMode, setIsEditMode] = useState(false)
  const [isHamburgerOpen, setIsHamburgerOpen] = useState(false)
  const [userInitialChartNames, setUserInitialChartNames] = useState()
  const [isLoading, setIsLoading] = useState(false)

  //chart states
  const [accounts, setAccounts] = useState([])
  const [expenses, setExpenses] = useState([])
  const [incomes, setIncomes] = useState([])
  const [username, setUsername] = useState("John Doe")

  const chartProps = { isEditMode, accounts, expenses, incomes }

  //chart effects
  useEffect(() => {
    const getUserName = async () => {
      const username = await fetchUserName()
      setUsername(username)
    }
    loadLayouts()
    getUserName()
  }, [])

  useEffect(() => {
    const getAccounts = async () => {
      const updatedAccounts = await fetchAccounts()
      setAccounts(updatedAccounts)
    }
    getAccounts()
  }, [username])

  useEffect(() => {
    const getTransactions = async () => {
      const updatedExpenses = []
      await Promise.all(accounts.map(async (acc) => {
        updatedExpenses.push(...await fetchExpensesWithCurrency(acc.accountId))
      }))

      const updatedIncomes = []
      await Promise.all(accounts.map(async (acc) => {
        updatedIncomes.push(...await fetchIncomesWithCurrency(acc.accountId))
      }))

      setExpenses(updatedExpenses)
      setIncomes(updatedIncomes)
    }

    if (accounts[0]) {
      getTransactions()
    }
  }, [accounts])
  //\
  // save layout
  const loadLayouts = async () => {
    return setUserInitialChartNames(await fetchLayouts())
  }


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
        saveChoosenChartsForUser={saveChoosenChartsForUser}
        username={username} />
      <TransactionSidebar />
      {isHamburgerOpen &&
        <HamburgerMenu
          isEditMode={isEditMode}
          setIsEditMode={setIsEditMode}
          chosenLayout={chosenLayout}
          setChosenLayout={setChosenLayout} />
      }      
      <div className="dashboard-container">
        {choosenCharts && choosenCharts.map((choosenChart, segmentIndex) => (
          <React.Fragment key={segmentIndex}>
            {accounts[0] && React.cloneElement(choosenChart.chart, { ...chartProps, segmentIndex, chosenLayout, choosenCharts, possibleChartsBySegment, setChoosenCharts })}
          </React.Fragment>
        ))}
      </div>
    </div>
  );
}