export const fetchUserInfo = async () => {
    const response = await fetch("api/user", {
        method: "Get",
        headers: {},
        credentials: "include",
    });
    if (!response.ok) 
        throw new Error(`Fetching userInfo went wrong`);

    const userInfo = await response.json();

    return userInfo;
};

export const fetchUserInfoChange = async (userInfo) => {
    const response = await fetch("api/user", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(userInfo)
    });
    if (!response.ok) 
        throw new Error(`Fetching userInfo went wrong`);

    return true;
};
export const fetchPasswordChange = async (password) => {
    const response = await fetch("api/user/password-change", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(password)
    });
    if (!response.ok) 
        throw new Error(response.statusText);

    return true;
};