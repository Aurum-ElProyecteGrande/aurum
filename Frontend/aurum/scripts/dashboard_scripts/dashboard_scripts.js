const apiUrl = "http://localhost:8080"

export const fetchExpenses = async (accId) => {
    const response = await fetch(`${apiUrl}/expenses/${accId}`)
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}

export const fetchExpensesByDate = async (accId, startDate, endDate) => {
    const response = await fetch(`${apiUrl}/expenses/${accId}/${startDate}/${endDate}`)
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}

export const fetchIncome = async (accId) => {
    const response = await fetch(`${apiUrl}/income/${accId}`)
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}

export const fetchIncomeByDate = async (accId, startDate, endDate) => {
    const response = await fetch(`${apiUrl}/income/${accId}/?startDate=${startDate}&endDate=${endDate}`)
    if (!response.ok) throw new Error(`Fetching expenses for account: ${accId} went wrong`)
    const expenses = await response.json()
    return expenses
}

export const fetchAccounts = async (userId) => {
    const response = await fetch(`${apiUrl}/account/${userId}`)
    if (!response.ok) throw new Error(`Fetching accounts for user: ${accId} went wrong`)
    const accounts = await response.json()
    return accounts
}

export const fetchBalance = async (accId) => {
    const response = await fetch(`${apiUrl}/balance/${accId}`)
    if (!response.ok) throw new Error(`Fetching balance for account: ${accId} went wrong`)
    const balance = await response.json()
    return balance 
}