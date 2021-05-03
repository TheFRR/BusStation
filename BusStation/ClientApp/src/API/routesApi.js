const API_URL = "/api/Routes";
export const getRoutes = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};