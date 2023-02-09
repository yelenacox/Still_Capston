import { getToken } from "./AuthManager";

const apiUrl = "/api/page";

export const getUserPages = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            },
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get pages.",
                );
            }
        });
    });
};