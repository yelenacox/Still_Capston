import { getToken } from "./AuthManager";

const apiUrl = "/api/picture";

export const getAllPictures = () => {
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
                    "An unknown error occurred while trying to get pictures.",
                );
            }
        });
    });
};

export const getUserPictures = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/UserPictures`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            },
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get pictures.",
                );
            }
        });
    });
};

export const getPicById = (id) => {
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
                    "An unknown error occurred while trying to get picture.",
                );
            }
        });
    });
};

export const addPicture = (picture) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "content-type": "application/json",
            },
            body: JSON.stringify(picture),
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

export const editPicture = (picture) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${picture.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(picture),
        })
        .then((res) => {
            if (res.ok) {
                return res;
            } else if (res.status === 401) {
                throw new Error ("Unauthorized");
            } 
            else {
                throw new Error (
                    "An unknown error occurred while trying to edit picture.",
               )
                ;
            }
        }
        );
    });
}

export const deletePicture = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
         })
        .then((res) => {
            if (res.ok) {
                return res;
            } else if (res.status === 401) {
                throw new Error ("Unauthorized");
            } 
            else {
                throw new Error (
                    "An unknown error occurred while trying to delete picture.",
               )
                ;
            }
        }
        );
    });
}