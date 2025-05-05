import { createContext, useState } from "react"

const AuthContext = createContext({})

export const AuthProvider = ({ children }) => {

    const [auth, setAuth] = useState(() => {
        const token = localStorage.getItem('token');
        const user = JSON.parse(localStorage.getItem("user"));
        return token && user ? 
            {
                accessToken: token,
                email: user.email,
                firstName: user.firstName,
                lastName: user.lastName,
            }
            : {};
    })

    return (
        <AuthContext.Provider value={{auth, setAuth}}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthContext;
