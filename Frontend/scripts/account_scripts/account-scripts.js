const apiUrl = "/api";

export const fetchUpdateAccount = async (accId, accountDto) => {
    const response = await fetch(`${apiUrl}/account/${accId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(accountDto)
    });
    if (!response.ok) throw new Error(response.text())
    return true
}

export const fetchDeleteAccount = async (accId) => {
    const response = await fetch(`${apiUrl}/account/${accId}`, {
        method: "DELETE",
        headers: {},
        credentials: "include"
    });
    if (!response.ok) throw new Error(response.text())
    return true
}

export const fetchPostAccount = async (account) => {
    const response = await fetch(`${apiUrl}/account`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(account)
    })
    if (!response.ok) throw new Error(response.text())
    const accId = response.json()
    return accId
}