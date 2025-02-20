import { allCharts } from "./layouts";

const apiUrl = "/api";

export const fetchExpenses = async (accId) => {
	const response = await fetch(`${apiUrl}/expenses/${accId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchExpensesWithCurrency = async (accId) => {
    const response = await fetch(`${apiUrl}/expenses/currency/${accId}`,{
        method: "Get",
        headers:{},
        credentials: "include"
    })
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}
export const fetchExpensesByDate = async (accId, startDate, endDate) => {
	const response = await fetch(`${apiUrl}/expenses/${accId}/${startDate}/${endDate}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchIncome = async (accId) => {
	const response = await fetch(`${apiUrl}/income/${accId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchIncomesWithCurrency = async (accId) => {
    const response = await fetch(`${apiUrl}/income/currency/${accId}`,{
        method: "Get",
        headers:{},
        credentials: "include"
    })
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}

export const fetchIncomeByDate = async (accId, startDate, endDate) => {
	const response = await fetch(
		`${apiUrl}/income/${accId}/?startDate=${startDate}&endDate=${endDate}`,
		{
			method: "Get",
			headers: {},
			credentials: "include",
		}
	);
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchAccounts = async () => {
	const response = await fetch(`${apiUrl}/account`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching accounts for user: ${accId} went wrong`);
	const accounts = await response.json();
	return accounts;
};

export const fetchBalance = async (accId) => {
	const response = await fetch(`${apiUrl}/balance/${accId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching balance for account: ${accId} went wrong`);
	const balance = await response.json();
	return balance;
};

export const fetchBalanceForRange = async (accId, startDate, endDate) => {
    const response = await fetch(`${apiUrl}/balance/${accId}?startDate=${startDate}&endDate=${endDate}`,{
        method: "Get",
        headers:{},
        credentials: "include"
    })
    if (!response.ok) throw new Error(`Fetching balance for account: ${accId} went wrong`)
    const balance = await response.json()
    return balance
}

export const fetchLayouts = async (userId) => {
	const response = await fetch(`${apiUrl}/Layout/${userId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	const layouts = await response.json();
	return layouts;
};

export const fetchPostLayout = async (layoutDto) => {
	const response = await fetch(`${apiUrl}/Layout/${layoutDto.layoutName}`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(layoutDto),
	});
};

export const getIndexOfPossibleChart = (chartName) => {
	for (const charts of allCharts) {
		for (let i = 0; i < charts.length; i++) {
			if (charts[i].name == chartName) return i;
		}
	}
	return false;
};

export const fetchUserName = async () => {
	const response = await fetch(`${apiUrl}/user`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching accounts for user: ${accId} went wrong`);
	const userInfo = await response.json();

	return userInfo.userName;
};

export const displayCurrency =(amount, curCurrency) => {
    return amount.toLocaleString('hu-HU', { style: 'currency', currency: curCurrency })
};
