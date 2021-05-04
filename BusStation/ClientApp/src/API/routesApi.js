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
    return response;
};
export const deleteRoute = async (id) => {
    const routeApiUrl = `${API_URL}/${id}`
    const response = await fetch(routeApiUrl, {
        method: "DELETE",
    });
    return response;
}