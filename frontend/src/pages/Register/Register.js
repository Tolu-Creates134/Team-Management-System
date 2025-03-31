import React, { useRef, useState, useEffect, useContext } from 'react'
import { Box } from '@mui/material';
import { Typography, TextField, Button, Link } from '@mui/material';
import InputAdornment from '@mui/material/InputAdornment';
import EmailIcon from '@mui/icons-material/Email';
import PasswordIcon from '@mui/icons-material/Password';
import bgImage from '../../assets/bg-loginPage.jpg';
import './Register.css';
import { UserRoles } from '../../data/enums/UserRoles';
import MenuItem from '@mui/material/MenuItem';
import { registerUser } from '../../services/Users/User';
import { useNavigate } from 'react-router-dom';

const Register = () => {

  const navigate = useNavigate();

  // Track register form values
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [userName, setUserName] = useState('');
  const [role, setRole] = useState('Manager');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!firstName || !lastName || !userName || !password || !confirmPassword){
      alert('Please fill in all fields');
      return
    }

    if (password !== confirmPassword) {
      alert('Passwords do not match');
    }

    const registerBody = {
      firstName,
      lastName,
      userName,
      email,
      password,
      confirmPassword,
      role,
      createdDate: new Date().toISOString(),
      updatedDate: new Date().toISOString(),
    };

    try {
      const response = await registerUser(registerBody);
      console.log('User Registered', response);
      alert('Registration successfully');
      navigate('/')
    } catch (error) {
      console.error('Registration failed', error)
      alert(error.response.data.message)
    }
  };
  
  return (
    <Box
      className='RegisterPage'
      sx={{
        backgroundImage: `url(${bgImage})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
      }}
    >
      <Box className='RegisterContainer'>
        <Typography variant='h5'>Sign up now</Typography>
        <Typography>Start tracking your teams journey to succes!</Typography>

        <form
          style={{
            width: '100%',
            mt: 3,
            display: 'flex',
            flexDirection: 'column',
          }}
          onSubmit={handleSubmit}
        >
          <TextField
            label='First Name'
            type='text'
            variant='outlined'
            margin='normal'
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />

          <TextField
            label='Last Name'
            type='text'
            variant='outlined'
            margin='normal'
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />

          <TextField
            label='Username'
            type='text'
            variant='outlined'
            margin='normal'
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />

          <TextField
            id='filled-select-currency'
            select
            label='Select'
            defaultValue='Manager'
            helperText='Please select a role'
            variant='filled'
            onChange={(e) => setRole(e.target.value)}
            value={role}
          >
            {Object.values(UserRoles).map((role) => (
              <MenuItem key={role} value={role}>
                {role}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            label='Email'
            type='email'
            variant='outlined'
            margin='normal'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position='start'>
                    <EmailIcon />
                  </InputAdornment>
                ),
              },
            }}
          />

          <TextField
            label='Password'
            type='password'
            variant='outlined'
            margin='normal'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position='start'>
                    <PasswordIcon />
                  </InputAdornment>
                ),
              },
            }}
          />

          <TextField
            label='Confirm Password'
            type='password'
            variant='outlined'
            margin='normal'
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position='start'>
                    <PasswordIcon />
                  </InputAdornment>
                ),
              },
            }}
          />

          <Button 
            variant='contained' 
            color='primary' 
            sx={{ mt: 2 }}
            type="submit"
          >
            Sign Up
          </Button>
        </form>
      </Box>
    </Box>
  );
};

export default Register;
