"use client";
import React, { useState, useEffect } from "react";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import TransactionTable from "@/components/transactions/transaction_table/TRansactionTable";
import TransactionHeader from "@/components/transactions/transaction_header/TransactionHeader";
import { fetchAccounts, fetchExpenses, fetchIncome, fetchExpensesByDate, fetchIncomeByDate } from "@/scripts/dashboard_scripts/dashboard_scripts";


export default function TransactionsPage() {
  const [accounts, setAccounts] = useState([]);
  const [data, setData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(12);
  const [search, setSearch] = useState("");
  const [filteredData, setFilteredData] = useState([])
  const [sort, setSort] = useState({ key: null, direction: "ascending" });
  const [loading, setLoading] = useState(true);
  const [startDate, setStartDate] = useState("") 
  const [today, setToday] = useState("")
  
  const getAccounts = async () => {
    const updatedAccounts = await fetchAccounts()

    const accountsWithChecked = updatedAccounts.map((account) => ({
      ...account, 
      checked: true, 
    }));

    setAccounts(accountsWithChecked)
  }

  const fetchAccountData = async (acc) => {
    if (!acc.checked) 
      return [];
  
    const mergedData = [];
  
    const [expenses, incomes] = await Promise.all([
      startDate === today
        ? fetchExpenses(acc.accountId)
        : fetchExpensesByDate(acc.accountId, startDate, today),
  
      startDate === today
        ? fetchIncome(acc.accountId)
        : fetchIncomeByDate(acc.accountId, startDate, today),
    ]);
  
    expenses.forEach((e) => mergedData.push({ ...e, isExpense: true, accountName: acc.displayName }));
    incomes.forEach((i) => mergedData.push({ ...i, isExpense: false, accountName: acc.displayName }));
  
    return mergedData; 
  };
  
  const getData = async () => {
    try {
      setLoading(true);
  
      const allMergedData = await Promise.all(accounts.map(fetchAccountData));
  
      const mergedData = allMergedData.flat();
  
      setData(mergedData);
      setFilteredData(mergedData);
  
      const startTime = Date.now();
      const elapsedTime = Date.now() - startTime;
      if (elapsedTime < 2000) {
        await new Promise((resolve) => setTimeout(resolve, 2000 - elapsedTime));
      }
    } catch (error) {
      console.error("Error fetching data:", error);
    } finally {
      setLoading(false);
    }
  };
  // Fetch data
  useEffect(() => {
    getAccounts()

    const today = new Date().toISOString().split('T')[0];
    setToday(today);
    setStartDate(today);
  }, []);

  useEffect(() => {
    getData();
  }, [accounts, startDate]);

  // Filter data when search changes
  useEffect(() => {
    let updatedFilteredData = data;

    if (search) {
      updatedFilteredData = data.filter(
        (d) =>
          d.category.name.toLowerCase().includes(search.toLowerCase()) ||
          d.subcategory?.name.toLowerCase().includes(search.toLowerCase()) ||
          d.label.toLowerCase().includes(search.toLowerCase())
      );
    }

    setFilteredData(updatedFilteredData);
  }, [search, data]);

  // Sort data when sort key or direction changes
  useEffect(() => {
    let updatedFilteredData = [...filteredData];

    if (sort.key !== null) {
      updatedFilteredData.sort((a, b) => {
        const keyParts = sort.key.split('.');
        const aValue = keyParts.reduce((acc, part) => acc && acc[part], a);
        const bValue = keyParts.reduce((acc, part) => acc && acc[part], b);

        if (aValue < bValue) {
          return sort.direction === "ascending" ? -1 : 1;
        }
        if (aValue > bValue) {
          return sort.direction === "ascending" ? 1 : -1;
        }
        return 0;
      });
    }

    setFilteredData(updatedFilteredData);
  }, [sort, data]);


  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;

  const currentData = filteredData.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(filteredData.length / itemsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  const handleSearchChange = (e) => {
    setSearch(e.target.value);
  };

  const onStartDateChange = (e) => {
    setStartDate(e.target.value)
  };

  const handleCheckboxChange = (accountId) => {
    const updatedAccounts = accounts.map(account => 
      account.accountId === accountId 
        ? { ...account, checked: !account.checked } 
        : account 
    );
  
    setAccounts(updatedAccounts);
  };

  

  const handleSort = (key) => {
    let direction = 'ascending';

    if (sort.key === key && sort.direction === 'ascending') {
      direction = 'descending';
    }

    setSort({ key, direction });
  };


  return (
    <section className="transactions">
      <TransactionSidebar />
      <TransactionHeader onSearchChange={handleSearchChange} startDate={startDate} onStartDateChange={onStartDateChange} accountOptions={accounts} handleCheckboxChange={handleCheckboxChange} />
      {loading ?
        <div className="loading wrapper">
        </div>
        :
        <div className="transactions-container wrapper">
          <TransactionTable data={currentData} onClick={handleSort} />
          <div className="transactions-pagination">
            <button
              className="primary-button"
              onClick={() => handlePageChange(currentPage - 1)}
              disabled={currentPage === 1}
            >
              Previous
            </button>

            <span>
              Page {currentPage} of {totalPages}
            </span>

            <button
              className="primary-button"
              onClick={() => handlePageChange(currentPage + 1)}
              disabled={currentPage === totalPages}
            >
              Next
            </button>
          </div>
        </div>}
    </section>
  );
}