import React from 'react'
import { useLocation, Navigate, Outlet } from 'react-router-dom'
import useAuth from '../hooks/useAuth'

const RequireAuth = () => {
    const { auth } = useAuth();
    const location = useLocation();
  return (
    auth?.accessToken
      ? <Outlet/> // represents all the child components of Require Auth
      : <Navigate to='/' state={{ from: location}} replace/>
   
  )
}

export default RequireAuth