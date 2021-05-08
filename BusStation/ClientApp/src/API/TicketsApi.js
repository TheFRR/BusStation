const API_URL = "/api/Tickets";

export const getTickets = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};

export const insertTickets = async (ticket) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(ticket),
    });
    return response;
};

export const deleteTicket = async (id) => {
    const ticketApiUrl = `${API_URL}/${id}`
    const response = await fetch(ticketApiUrl, {
        method: "DELETE",
    });
    return response;
};

export const updateTicket = async (ticket) => {
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
