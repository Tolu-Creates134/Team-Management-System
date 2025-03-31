import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

/**
 * @param {string} email 
 * @param {string} password 
 * @returns 
 */
const loginUser = async (email, password) => {
    const response = await axios.post(`http://localhost:5170/api/user/login`, {
        email,
        password,
    });
    return response.data;
};

export {loginUser};


/**
 * @typedef {Object} RegisterRequest
 * @property {string} firstName
 * @property {string} lastName
 * @property {string} username
 * @property {string} email
 * @property {string} password
 * @property {string} confirmPassword
 * @property {string} role
 * @property {string} createdDate
 * @property {string} updatedDate
 */

/**
 * @param {RegisterRequest} body
 */
// body represents registerForm details objects
const registerUser = async (body) => {
    const response = await axios.post(`http://localhost:5170/api/user/register`, body);

    return response.data
}

export  {registerUser};
