const API_URL = "/api/BoughtTickets";

export const getBoughtTicket = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};

export const insertBoughtTicket = async (ticket) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(ticket),
    });
    return response;
};

export const deleteBoughtTicket = async (id) => {
    const ticketApiUrl = `${API_URL}/${id}`
    const response = await fetch(ticketApiUrl, {
        method: "DELETE",
    });
    return response;
};

export const updateBoughtTicket = async (ticket) => {
    const ticketApiUrl = `${API_URL}/${ticket.id}`;
    const response = await fetch(ticketApiUrl, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(ticket),
    });
    return response;
};
