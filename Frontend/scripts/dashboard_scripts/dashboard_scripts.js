import { allCharts } from "./layouts";

const exchangeApiKey = "fca_live_1D7AbjPoPrq9dGaRwMjLdVWlpBTrJ3cFMK25qOYO";
const exchangeApiUrl = `https://api.freecurrencyapi.com/v1/latest?apikey=${exchangeApiKey}`;
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
	if (!response.ok) throw new Error(`Fetching accounts for user went wrong`);
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
	const response = await fetch(
		`${apiUrl}/balance/${accId}/range/?startDate=${startDate}&endDate=${endDate}`,
		{
			method: "Get",
			headers: {},
			credentials: "include",
		}
	);
	if (!response.ok) throw new Error(`Fetching balance for account: ${accId} went wrong`);
	const balance = await response.json();
	return balance;
};

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
	if (!response.ok) return false;
	return true;
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

export const displayCurrency = (amount, curCurrency) => {
	return amount.toLocaleString("hu-HU", { style: "currency", currency: curCurrency });
};

export const shortenTitle = (title, maxLength) => {
	title = title.trim();
	if (!title) return;
	if (title.length <= maxLength) {
		return title;
	}

	const words = title.split(" ");
	let wordIndex = 0;
	let totalChar = 0;

	for (let i = 0; i < words.length; i++) {
		totalChar += words[i].length + 1;
		if (totalChar >= maxLength) {
			wordIndex = i;
			break;
		}
	}

	const wordsOfShortTitle = [];
	for (let i = 0; i < wordIndex; i++) {
		wordsOfShortTitle.push(words[i]);
	}

	let shortenedTitle = wordsOfShortTitle.join(" ");
	shortenedTitle = shortenedTitle + "...";
	return shortenedTitle;
};

export const fetchCurrencyExchanges = async (base, currencies) => {
	let currencyList = currencies.join(",");
	let url = exchangeApiUrl;
	if (base) url += `&base_currency=${base}`;
	if (currencies) url += `&currencies=${currencyList}`;

	const response = await fetch(url);
	if (!response.ok)
		throw new Error(`Fetching exchange rates for ${base} to ${currencyList} went wrong`);
	const exchangeRates = await response.json();
	return exchangeRates;
};

export const convertExchangeRate = (amount, exchangeRates, originalCurrencyCode) => {
	if (!originalCurrencyCode in exchangeRates) return amount;
	let exchangeRate = exchangeRates[originalCurrencyCode];
	return amount / exchangeRate;
};

export const fetchIncomeCategories = async () => {
	const response = await fetch(`${apiUrl}/categories/income`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching income categories went wrong`);
	const categories = await response.json();
	return categories;
};

export const fetchExpenseCategories = async () => {
	const response = await fetch(`${apiUrl}/categories/expense`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expense categories went wrong`);
	const categories = await response.json();
	return categories;
};

export const fetchPostIncome = async (income) => {
	const response = await fetch(`${apiUrl}/income`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(income),
	});
	if (!response.ok) {
		console.error(`Creating income went wrong`);
		return false;
	}
	return true;
};

export const fetchPostExpense = async (expense) => {
	const response = await fetch(`${apiUrl}/expenses`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(expense),
	});
	if (!response.ok) {
		console.error(`Creating expense went wrong`);
		return false;
	}
	return true;
};

export const fetchRegularExpenses = async (accId) => {
	const response = await fetch(`${apiUrl}/expenses/regulars/${accId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const expenses = await response.json();
	return expenses;
};
export const fetchRegularIncome = async (accId) => {
	const response = await fetch(`${apiUrl}/income/regulars/${accId}`, {
		method: "Get",
		headers: {},
		credentials: "include",
	});
	if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`);
	const incomes = await response.json();
	return incomes;
};

export const regularity = {
	Daily: "Daily",
	Weekly: "Weekly",
	Monthly: "Monthly",
};

const convertToModifyRegularIncomeDto = (data) => {
	return {
		AccountId: data.accountId,
		CategoryId: data.category.categoryId,
		Label: data.label,
		Amount: data.amount,
		StartDate: new Date(data.startDate).toISOString(),
		Regularity: regularity[data.regularity],
	};
};

const convertToModifyRegularExpenseDto = (data) => {
	return {
		AccountId: data.accountId,
		CategoryId: data.category.categoryId,
		SubCategoryName: data.subcategory?.name || null,
		Label: data.label,
		Amount: data.amount,
		StartDate: new Date(data.startDate).toISOString(),
		Regularity: data.regularity,
	};
};

export const fetchUpdateRegularIncome = async (regularIncome) => {
	const data = convertToModifyRegularIncomeDto(regularIncome);

	const response = await fetch(`${apiUrl}/income/regulars/${regularIncome.regularId}`, {
		method: "PUT",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(data),
	});
	if (!response.ok)
		throw new Error(`Fetching expenses for account: ${regularIncome.regularId} went wrong`);
	const incomes = await response.json();
	return incomes;
};

export const fetchUpdateRegularExpense = async (regularExpense) => {
	const data = convertToModifyRegularExpenseDto(regularExpense);

	const response = await fetch(`${apiUrl}/expenses/regulars/${regularExpense.regularId}`, {
		method: "PUT",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(data),
	});
	if (!response.ok)
		throw new Error(`Fetching expenses for account: ${regularExpense.regularId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchPostRegularIncome = async (regularIncome) => {
	const response = await fetch(`${apiUrl}/income/regulars`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(regularIncome),
	});
	if (!response.ok)
		throw new Error(`Fetching expenses for account: ${regularIncome.accountId} went wrong`);
	const incomes = await response.json();
	return incomes;
};

export const fetchPostRegularExpense = async (regularExpense) => {

	const response = await fetch(`${apiUrl}/expenses/regulars`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		credentials: "include",
		body: JSON.stringify(regularExpense),
	});
	if (!response.ok)
		throw new Error(`Fetching expenses for account: ${regularExpense.regularId} went wrong`);
	const expenses = await response.json();
	return expenses;
};

export const fetchDeleteRegularExpense = async (id) => {

	const response = await fetch(`${apiUrl}/expenses/regulars/${id}`, {
		method: "DELETE",
		headers: {},
		credentials: "include",
	});
	if (!response.ok)
		throw new Error(`Deleting went wrong`);
};

export const fetchDeleteRegularIncome = async (id) => {

	const response = await fetch(`${apiUrl}/income/regulars/${id}`, {
		method: "DELETE",
		headers: {},
		credentials: "include",
	});
	if (!response.ok)
		throw new Error(`Deleting went wrong`);
};
