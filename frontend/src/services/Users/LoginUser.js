import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

const loginUser = async (email, password) => {
    try {
        // Send POST request with user credentials
        const response = await axios.post(`${API_URL}/user/login`, { email, password })

        // return data to frontend
        return response.data; 
    } 
    catch (error) {
        const status = error.response.status;
        const message = error.response.data.message || "Login failed. Please try again.";

        if (error.resposne){
            // Checks the status code of error message
            if (status === 400) throw new Error("Invalid request. Please check your input.");
            if (status === 401) throw new Error("Unauthorized: Incorrect email or password.");
            if (status === 500) throw new Error("Server error. Please try again later.");

            throw new Error(message)
        }
        else if (error.request) {
            throw new Error("No response from server. Please check your internet connection.");
        } 
        else {
            throw new Error("An unexpected error occurred.");
        }
    }
};

export default loginUser