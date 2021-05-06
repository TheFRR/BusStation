const API_URL = "/api/Flights";

export const getFlights = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};

export const insertFlight = async (flight) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(flight),
    });
    return response;
};

export const deleteFlight = async (id) => {
    const flightApiUrl = `${API_URL}/${id}`
    const response = await fetch(flightApiUrl, {
        method: "DELETE",
    });
    return response;
};

export const updateFlight = async (flight) => {
    const flightApiUrl = `${API_URL}/${flight.id}`;
    const response = await fetch(flightApiUrl, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(flight),
    });
    return response;
};