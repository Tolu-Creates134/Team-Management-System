import React, { useRef, useState, useEffect, useContext } from 'react'
import Box from '@mui/material/Box';
import { useNavigate } from "react-router-dom"; 
import { Typography, TextField, Button } from '@mui/material';
import { Link } from "react-router-dom";
import './Login.css'
import InputAdornment from '@mui/material/InputAdornment';
import EmailIcon from '@mui/icons-material/Email';
import PasswordIcon from '@mui/icons-material/Password';
import bgImage from '../../assets/bg-loginPage.jpg'
import AuthContext from '../../context/AuthProvider';
import loginUser from '../../services/Users/User';

const Login = () => {

    const { setAuth } = useContext(AuthContext)
    const errorRef = useRef();
    const navigate = useNavigate();

    const [email, setEmail] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');

    useEffect(() => {

    },[email, pwd])


    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!email.trim() || !pwd.trim()) {
            setErrMsg("Email and password are required.");
            return;
        }
        
        try {
            const data = await loginUser(email, pwd); // Call API function
    
            console.log("Login Successful:", data);
    
            // Store tokens in localStorage (or sessionStorage)
            localStorage.setItem("access_token", data.accessToken);
            localStorage.setItem("refresh_token", data.refreshToken);
    
            // Redirect user to home page after successful login
            navigate("/home");
        } catch (error) {
            alert(error.message); // Show error to user
        }
        
        setEmail('')
        setPwd('')
    }

  return (
    <Box 
        className='LoginPage' 
        sx={{
            backgroundImage: `url(${bgImage})`,
            backgroundSize: 'cover',
            backgroundPosition: 'center',
        }}
    >
        <Box className='LoginContainer'>
            <Typography variant='h5'>
                Sign In with Email
            </Typography>
            <Typography>
                Start tracking your teams journey to succes!
            </Typography>

            {/* Form starts here */}
            <form 
                style={{ 
                    width: '100%', 
                    mt: 3, 
                    display: 'flex', 
                    flexDirection: 'column' 
                }}
                onSubmit={handleSubmit}
            >
                <TextField  
                    label="Email" 
                    type="email" 
                    variant="outlined" 
                    margin="normal"
                    onChange={(e) => setEmail(e.target.value)}
                    value={email}
                    required
                    slotProps={{
                        input: {
                          startAdornment: (
                            <InputAdornment position="start">
                              <EmailIcon/>
                            </InputAdornment>
                          ),
                        },
                    }}
                />

                {errMsg && (
                    <Typography sx={{ color: "red", fontSize: "0.9rem" }} ref={errorRef}>
                        {errMsg}
                    </Typography>
                )}

                <TextField  
                    label="Password" 
                    type="password" 
                    variant="outlined" 
                    margin="normal"
                    onChange={(e) => setPwd(e.target.value)}
                    value={pwd}
                    slotProps={{
                        input: {
                          startAdornment: (
                            <InputAdornment position="start">
                              <PasswordIcon/>
                            </InputAdornment>
                          ),
                        },
                    }}
                />

                <Typography variant='h7'>
                    <Link underline="none" sx={{ fontWeight: 'bold' }}>
                        Forgot password?
                    </Link>
                </Typography>

                <Button 
                    variant="contained" 
                    color="primary"  
                    sx={{ mt: 2 }}
                    type="submit"
                >
                    Sign In
                </Button>

                <Typography sx={{ marginTop: '1.5rem' }}>
                    Not yet signed up?{' '}
                    <Link to="/register" color="primary" underline="none" sx={{ fontWeight: 'bold' }}>
                        Register here
                    </Link>
                </Typography>
            </form>
        </Box>
    </Box>
  )
}

export default Login