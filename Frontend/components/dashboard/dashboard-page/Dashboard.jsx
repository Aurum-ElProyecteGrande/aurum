"use client";

import React from "react";
import { useState, useEffect } from "react";
import HamburgerMenu from "../hamburger-menu";
import Header from "../header";
import ChangeChartForm from "../change-chart-form";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import { layouts } from "@/scripts/dashboard_scripts/layouts";
import { fetchAccounts, fetchExpenses, fetchIncome, fetchLayouts, fetchPostLayout, fetchUserName } from "@/scripts/dashboard_scripts/dashboard_scripts";
import { getIndexOfPossibleChart } from "@/scripts/dashboard_scripts/dashboard_scripts";
import InfoToast from "@/components/toasts/info-toast";
import OpenAddModal from "@/components/add_modal/open-add-modal";
import AddModal from "@/components/add_modal/add-modal";

export default function Dashboard() {

  const [chosenLayout, setChosenLayout] = useState("basic")
  const [possibleChartsBySegment, setPossibleChartsBySegment] = useState(layouts["basic"].possibleCharts)
  const [chosenCharts, setChosenCharts] = useState()  //layouts["basic"].initialCharts
  const [isEditMode, setIsEditMode] = useState(false)
  const [isHamburgerOpen, setIsHamburgerOpen] = useState(false)
  const [userInitialChartNames, setUserInitialChartNames] = useState()
  const [isLoading, setIsLoading] = useState(true)
  const [isToast, setIsToast] = useState(false)
  const [toastText, setToastText] = useState("")
  const [toastType, setToastType] = useState("") //success / fail / null
  const [loadedCharts, setLoadedCharts] = useState([])

  //add-modal
  const [isAddModal, setIsAddModal] = useState(false)


  //chart states
  const [accounts, setAccounts] = useState([])
  const [expenses, setExpenses] = useState([])
  const [incomes, setIncomes] = useState([])
  const [username, setUsername] = useState("John Doe")

  const chartLoaded = (chartName) => {
    let updatedLoadedCharts = [...loadedCharts]
    updatedLoadedCharts = updatedLoadedCharts.map(c => c.name === chartName ? { ...c, isLoaded: true } : c)
    setLoadedCharts(updatedLoadedCharts)
  }

  const chartProps = { isEditMode, accounts, expenses, incomes, chartLoaded }

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
        updatedExpenses.push(...await fetchExpenses(acc.accountId))
      }))

      const updatedIncomes = []
      await Promise.all(accounts.map(async (acc) => {
        updatedIncomes.push(...await fetchIncome(acc.accountId))
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
        setChosenCharts(updatedUserInitialCharts)
      } else {
        setChosenCharts(layouts[chosenLayout].initialCharts)
      }

    } else {
      setChosenCharts(layouts[chosenLayout].initialCharts)
    }

    setPossibleChartsBySegment(updatedPossibleChartsBySegment)

  }, [chosenLayout, userInitialChartNames])

  //save & reload chosen layout
  const saveChosenChartsForUser = async () => {
    const chosenChartNames = chosenCharts.map(chart => chart.name)
    const layoutDto = {
      layoutName: chosenLayout,
      charts: chosenChartNames
    }

    let isSaved = await fetchPostLayout(layoutDto)
    await loadLayouts()

    isSaved ? useInfoToast("Layout saved!", "success") : useInfoToast("Saving failed!", "fail")
  }

  //handle dashboard loading
  useEffect(() => {
    if (chosenCharts) {
      let updatedLoadedCharts = []
      chosenCharts.forEach(c => {
        updatedLoadedCharts.push(
          {
            name: c.name,
            isLoaded: false
          }
        )
      })
      setLoadedCharts(updatedLoadedCharts)
    }
  }, [chosenCharts])

  useEffect(() => {
    if (loadedCharts) {
      if (loadedCharts.every(c => c.isLoaded)) setIsLoading(false)
    }
  }, [loadedCharts])

  const useInfoToast = (text, type) => {
    setToastType(type)
    setToastText(text)
    setIsToast(true)
    setTimeout(() => setIsToast(false), 5000);
  }


  return (
    <div className="dashboard page">
      <Header
        setIsHamburgerOpen={setIsHamburgerOpen}
        isHamburgerOpen={isHamburgerOpen}
        isEditMode={isEditMode}
        chosenLayout={chosenLayout}
        saveChosenChartsForUser={saveChosenChartsForUser}
        username={username} />
      <TransactionSidebar />
      {isHamburgerOpen &&
        <HamburgerMenu
          isEditMode={isEditMode}
          setIsEditMode={setIsEditMode}
          chosenLayout={chosenLayout}
          setChosenLayout={setChosenLayout} />
      }
      {isLoading ?
        <div className="loader"></div>
        :
        <div className="dashboard-container">
          {chosenCharts && chosenCharts.map((chosenChart, segmentIndex) => (
            <React.Fragment key={segmentIndex}>
              {accounts[0] && React.cloneElement(chosenChart.chart, { ...chartProps, segmentIndex, chosenLayout, chosenCharts: chosenCharts, possibleChartsBySegment, setChosenCharts: setChosenCharts })}
            </React.Fragment>
          ))}
        </div>
      }
      <OpenAddModal setIsAddModal={setIsAddModal} />
      {isAddModal &&
        <AddModal accounts={accounts} useInfoToast={useInfoToast} setIsAddModal={setIsAddModal}/>
      }
      <InfoToast toastText={toastText} isToast={isToast} setIsToast={setIsToast} toastType={toastType} />
    </div>
  );
}