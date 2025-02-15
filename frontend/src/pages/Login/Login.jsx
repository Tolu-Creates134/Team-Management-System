import React, { useRef, useState, useEffect } from 'react'
import Box from '@mui/material/Box';
import { Typography, TextField, Button, FormGroup } from '@mui/material';
import { Link } from "react-router-dom";
import './Login.css'
import InputAdornment from '@mui/material/InputAdornment';
import EmailIcon from '@mui/icons-material/Email';
import PasswordIcon from '@mui/icons-material/Password';
import bgImage from '../../assets/bg-loginPage.jpg'

const Login = () => {

    const emailRef = useRef();
    const errorRef = useRef();

    const [email, setEmail] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    useEffect(() => {

    },[email, pwd])


    const handleSubmit = async (e) => {
        e.preventDefault();
        setEmail('')
        setPwd('')
        console.log(email, pwd)
        setSuccess(true)
        console.log(true)
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
                <Typography
                    sx={{
                        color: 'red'
                    }}
                >
                    {errMsg}
                </Typography>

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