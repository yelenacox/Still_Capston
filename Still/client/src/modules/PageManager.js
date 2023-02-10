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

export const getPageById = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            },
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get page.",
                );
            }
        });
    });
};

export const addPage = (page, pictureIds) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "content-type": "application/json",
            },
            body: JSON.stringify({"page":{...page},"pictureIds":pictureIds}),
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else if (res.status === 401) {
                throw new Error ("Unauthorized");
            } else {
                throw new Error (
                    "An unknown error occurred while trying to save a new picture.",
                );
            }
        });
    });
}