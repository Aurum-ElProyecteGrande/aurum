"use client";
import React, { useState, useEffect } from "react";
import TransactionHeader from "@/components/transactions/transaction_header/TransactionHeader";
import TransactionCards from "@/components/transactions/transaction_cards/TransactionsCard";
import { fetchAccounts, fetchExpenses, fetchIncome } from "@/scripts/dashboard_scripts/dashboard_scripts";
import MobileBottomBar from "@/components/mobile_dashboard_page/mobile_bottombar/MobileBottomBar";


export default function TransactionsPage() {
    const [accounts, setAccounts] = useState([]);
    const [accountsWithChecked, setAccountsWithChecked] = useState([]);
    const [fullData, setFullData] = useState([]);
    const [search, setSearch] = useState("");
    const [semiFilteredData, setSemiFilteredData] = useState([])
    const [filteredData, setFilteredData] = useState([])
    const [sort, setSort] = useState({ key: "date", direction: "descending" });
    const [loading, setLoading] = useState(true);
    const [startDate, setStartDate] = useState("")
    const [today, setToday] = useState("")

    const getAccounts = async () => {
        const updatedAccounts = await fetchAccounts()

        const accountsWithChecked = updatedAccounts.map((account) => ({
            ...account,
            checked: true,
        }));

        setAccounts(updatedAccounts)
        setAccountsWithChecked(accountsWithChecked)
    }

    const getData = async () => {
        try {
            setLoading(true)
            const mergedData = [];

            for (const acc of accounts) {
                const [expenses, incomes] = await Promise.all([
                    fetchExpenses(acc.accountId),
                    fetchIncome(acc.accountId),
                ]);

                expenses.forEach((e) => mergedData.push({ ...e, isExpense: true, accountName: acc.displayName }));
                incomes.forEach((i) => mergedData.push({ ...i, isExpense: false, accountName: acc.displayName }));
            }

            setFullData(mergedData);
            setSemiFilteredData(mergedData)
            const startTime = Date.now();
            const elapsedTime = Date.now() - startTime;
            if (elapsedTime < 2000) {
                await new Promise((resolve) => setTimeout(resolve, 2000 - elapsedTime));
            }
        } catch (error) {
            console.error("Error fetching data:", error);
        }
        finally {
            setLoading(false)
        }
    };

    useEffect(() => {
        getAccounts()

        const today = new Date().toISOString().split('T')[0];
        setToday(today);
        setStartDate(today);
    }, []);

    useEffect(() => {
        getData();

    }, [accounts]);


    useEffect(() => {
        const checkedAccounts = accountsWithChecked.filter(a => a.checked);
        let updatedData = fullData.filter(d => checkedAccounts.find(a => a.displayName === d.accountName));

        if (startDate !== today)
            updatedData = updatedData.filter(d => d.date >= startDate);


        setSemiFilteredData(updatedData);
    }, [accountsWithChecked, startDate]);


    useEffect(() => {
        let updatedData = [...semiFilteredData];

        if (search)
            updatedData = updatedData.filter(d =>
                d.category.name.toLowerCase().includes(search.toLowerCase()) ||
                d.subcategory?.name.toLowerCase().includes(search.toLowerCase()) ||
                d.label.toLowerCase().includes(search.toLowerCase())
            );


        if (sort.key) {
            updatedData.sort((a, b) => {
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

        setFilteredData(updatedData);
    }, [search, semiFilteredData]);

    const handleSearchChange = (e) => {
        setSearch(e.target.value);
    };

    const onStartDateChange = (e) => {
        setStartDate(e.target.value)
    };

    const handleCheckboxChange = (accountId) => {
        const updatedAccounts = accountsWithChecked.map(account =>
            account.accountId === accountId
                ? { ...account, checked: !account.checked }
                : account
        );

        setAccountsWithChecked(updatedAccounts);
    };

    return (
        <section className="transactions">
            <TransactionHeader onSearchChange={handleSearchChange} startDate={startDate} onStartDateChange={onStartDateChange} accountOptions={accountsWithChecked} handleCheckboxChange={handleCheckboxChange} />
            {loading ?
                <div className="loading wrapper">
                </div>
                : <TransactionCards data={filteredData} />}
            <MobileBottomBar />
        </section>
    );
}