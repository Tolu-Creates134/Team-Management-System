import React from 'react'
import { Box } from '@mui/material'
import { Typography, TextField, Button, Link } from '@mui/material';
import InputAdornment from '@mui/material/InputAdornment';
import EmailIcon from '@mui/icons-material/Email';
import PasswordIcon from '@mui/icons-material/Password';
import bgImage from '../../assets/bg-loginPage.jpg'
import './Register.css'
import { UserRoles } from '../../data/enums/UserRoles';
import MenuItem from '@mui/material/MenuItem';

const Register = () => {
  return (
    <Box 
        className='RegisterPage' 
        sx={{
            backgroundImage: `url(${bgImage})`,
            backgroundSize: 'cover',
            backgroundPosition: 'center',
        }}
    >
        <Box className='LoginContainer'>
            <Typography variant='h5'>
                Sign up now
            </Typography>
            <Typography>
                Start tracking your teams journey to succes!
            </Typography>

            <Box sx={{ width: '100%', mt: 3, display: 'flex', flexDirection: 'column' }}>
                <TextField
                    label="First Name"
                    type='text'
                    variant='outlined'
                    margin='normal'
                />

                <TextField
                    label="Last Name"
                    type='text'
                    variant='outlined'
                    margin='normal'
                />

                <TextField
                    label="Username"
                    type='text'
                    variant='outlined'
                    margin='normal'
                />

                <TextField
                    id="filled-select-currency"
                    select
                    label="Select"
                    defaultValue="Manager"
                    helperText="Please select a role"
                    variant="filled"
                >
                    {Object.values(UserRoles).map((role) => (
                        <MenuItem key={role} value={role}>
                            {role}
                        </MenuItem>
                    ))}
                </TextField>

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

                <TextField  
                    label="Confirm Password" 
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

                <Button 
                    variant="contained" 
                    color="primary"  
                    sx={{ mt: 2 }}
                >
                    Sign Up
                </Button>
            </Box>
        </Box>
    </Box>
  )
}

export default Register