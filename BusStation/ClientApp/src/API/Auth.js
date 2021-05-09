const API_URL = "/api/Account";

export const signup = async (signupForm) => {
    const response = await fetch(API_URL + "/Register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(signupForm),
    });
    return response;
};

export const signin = async (signinForm) => {
    const response = await fetch(API_URL + "/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(signinForm),
    });
    return response;
};

export const signout = async () => {
    const response = await fetch(API_URL + "/LogOff", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    });
    return response;
};

export const getAuthInfo = async () => {
    const response = await fetch(API_URL + "/isAuthenticated", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    });
    return response.json();
};

export const getUser = async () => {
    const response = await fetch(API_URL + "/currentUser", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    });
    return response;
};
