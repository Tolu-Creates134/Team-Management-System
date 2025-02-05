import React from 'react'
import Box from '@mui/material/Box';
import { Typography, TextField, Button, Link } from '@mui/material';
import './Login.css'
import InputAdornment from '@mui/material/InputAdornment';
import EmailIcon from '@mui/icons-material/Email';
import PasswordIcon from '@mui/icons-material/Password';
import bgImage from '../../assets/bg-loginPage.jpg'

const Login = () => {
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

            <Box sx={{ width: '100%', mt: 3, display: 'flex', flexDirection: 'column' }}>
                <TextField  
                    label="Email" 
                    type="email" 
                    variant="outlined" 
                    margin="normal"
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

                <TextField  
                    label="Password" 
                    type="password" 
                    variant="outlined" 
                    margin="normal"
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
                >
                    Sign In
                </Button>

                <Typography sx={{ marginTop: '1.5rem' }}>
                    Not yet signed up?{' '}
                    <Link color="primary" underline="none" sx={{ fontWeight: 'bold' }}>
                        Sign up here
                    </Link>
                </Typography>
            </Box>
        </Box>
    </Box>
  )
}

export default Login