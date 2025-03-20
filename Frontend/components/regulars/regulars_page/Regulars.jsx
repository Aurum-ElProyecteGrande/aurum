"use client";
import React, { useState, useEffect } from "react";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import RegularHeader from "../regular_header/RegularHeader";
import {
	fetchAccounts,
	fetchRegularExpenses,
	fetchRegularIncome,
	fetchUpdateRegularExpense,
	fetchUpdateRegularIncome
} from "@/scripts/dashboard_scripts/dashboard_scripts";
import RegularsTable from "../regulars_table/RegularsTable";

export default function Regulars() {
	const [accounts, setAccounts] = useState([]);
	const [accountsWithChecked, setAccountsWithChecked] = useState([]);
	const [fullData, setFullData] = useState([]);
	const [currentPage, setCurrentPage] = useState(1);
	const [itemsPerPage] = useState(14);
	const [search, setSearch] = useState("");
	const [semiFilteredData, setSemiFilteredData] = useState([]);
	const [filteredData, setFilteredData] = useState([]);
	const [sort, setSort] = useState({ key: null, direction: "ascending" });
	const [loading, setLoading] = useState(true);
	const [startDate, setStartDate] = useState("");
	const [today, setToday] = useState("");
	const [isEditing, setIsEditing] = useState(false);
	const [modifiedRows, setModifiedRows] = useState([]);

	const getAccounts = async () => {
		const updatedAccounts = await fetchAccounts();

		const accountsWithChecked = updatedAccounts.map((account) => ({
			...account,
			checked: true,
		}));

		setAccounts(updatedAccounts);
		setAccountsWithChecked(accountsWithChecked);
	};

	const getData = async () => {
		try {
			setLoading(true);
			const mergedData = [];

			for (const acc of accounts) {
				const [expenses, incomes] = await Promise.all([
					fetchRegularExpenses(acc.accountId),
					fetchRegularIncome(acc.accountId),
				]);

				expenses.forEach((e) =>
					mergedData.push({ ...e, isExpense: true, accountName: acc.displayName, accountId: acc.accountId })
				);
				incomes.forEach((i) =>
					mergedData.push({ ...i, isExpense: false, accountName: acc.displayName, accountId: acc.accountId })
				);
			}

			setFullData(mergedData);
			setSemiFilteredData(mergedData);
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

	useEffect(() => {
		getAccounts();

		const today = new Date().toISOString().split("T")[0];
		setToday(today);
		setStartDate(today);
	}, []);

	useEffect(() => {
		getData();
	}, [accounts]);

	useEffect(() => {
		const checkedAccounts = accountsWithChecked.filter((a) => a.checked);
		let updatedData = fullData.filter((d) =>
			checkedAccounts.find((a) => a.displayName === d.accountName)
		);

		if (startDate !== today) updatedData = updatedData.filter((d) => d.date >= startDate);

		setSemiFilteredData(updatedData);
	}, [accountsWithChecked, startDate]);

	useEffect(() => {
		let updatedData = [...semiFilteredData];

		if (search)
			updatedData = updatedData.filter(
				(d) =>
					d.category.name.toLowerCase().includes(search.toLowerCase()) ||
					d.subcategory?.name.toLowerCase().includes(search.toLowerCase()) ||
					d.label.toLowerCase().includes(search.toLowerCase())
			);

		if (sort.key) {
			updatedData.sort((a, b) => {
				const keyParts = sort.key.split(".");
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
	}, [search, sort, semiFilteredData]);

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
		setStartDate(e.target.value);
	};

	const handleCheckboxChange = (accountId) => {
		const updatedAccounts = accountsWithChecked.map((account) =>
			account.accountId === accountId ? { ...account, checked: !account.checked } : account
		);

		setAccountsWithChecked(updatedAccounts);
	};
	const handleEditing = async () => {
		if (isEditing)
			await Promise.all(
				modifiedRows.map(async (data) => {
					data.isExpense ?
						await fetchUpdateRegularExpense(data) :
						await fetchUpdateRegularIncome(data)
				})
			);


		setIsEditing(!isEditing);
	};

	const handleSelectChange = (index, value) => {
		const updatedRow = {
			...semiFilteredData[index],
			regularity: value,
		};

		setSemiFilteredData(current => {
			return current.map((data, innerIndex) => {
				if (innerIndex !== index)
					return data;

				return { ...data, regularity: value };
			});
		});


		const existingRowIndex = modifiedRows.findIndex((row) => row.index === index);

		if (existingRowIndex === -1) {
			setModifiedRows((prevRows) => [
				...prevRows,
				{ ...updatedRow, index },
			]);
		} else {
			const updatedRows = [...modifiedRows];
			updatedRows[existingRowIndex] = { ...updatedRow, index };
			setModifiedRows(updatedRows);
		}
	};


	const handleSort = (key) => {
		let direction = "ascending";

		if (sort.key === key && sort.direction === "ascending") {
			direction = "descending";
		}

		setSort({ key, direction });
	};

	return (
		<section className="transactions">
			<TransactionSidebar />
			<RegularHeader
				onSearchChange={handleSearchChange}
				startDate={startDate}
				onStartDateChange={onStartDateChange}
				accountOptions={accountsWithChecked}
				handleCheckboxChange={handleCheckboxChange}
				isEditing={isEditing}
				handleEditing={handleEditing}
			/>
			{loading ? (
				<div className="loader"></div>
			) : (
				<div className="transactions-container wrapper">
					<div className="transactions-pagination">
						<button
							className="primary-button"
							onClick={() => handlePageChange(currentPage - 1)}
							disabled={currentPage === 1}>
							Previous
						</button>

						<span>
							Page {currentPage} of {totalPages}
						</span>

						<button
							className="primary-button"
							onClick={() => handlePageChange(currentPage + 1)}
							disabled={currentPage === totalPages}>
							Next
						</button>
					</div>
					<RegularsTable data={currentData} onClick={handleSort} isEditing={isEditing} handleSelectChange={handleSelectChange} />
				</div>
			)}
		</section>
	);
}
