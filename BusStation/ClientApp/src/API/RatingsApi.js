const API_URL = "/api/Ratings";

export const getRatings = async () => {
    const response = await fetch(API_URL);
    return (await response.json());
};

export const insertRating = async (rating) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(rating),
    });
    return response;
};

export const deleteRating = async (id) => {
    const ratingApiUrl = `${API_URL}/${id}`
    const response = await fetch(ratingApiUrl, {
        method: "DELETE",
    });
    return response;
};

export const updateRating = async (rating) => {
    const ratingApiUrl = `${API_URL}/${rating.id}`;
    const response = await fetch(ratingApiUrl, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(rating),
    });
    return response;
};