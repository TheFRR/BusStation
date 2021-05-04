import { Route } from "react-router";

const API_URL = "/api/Routes";
export const getRoutes = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};
export const insertRoute = async (route) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(route),
    });
    await response;
    return response;
};