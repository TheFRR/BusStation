const API_URL = "http://localhost:52449/api/Routes";
export const getRoutes = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};