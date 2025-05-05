import { createTheme } from '@mui/material/styles';

const homeTheme = createTheme({
    palette: {
        mode: 'dark', // Always dark mode for home
        primary: {
            main: '#00C853', // Bright green (example)
        },
        secondary: {
            main: '#FFD600', // Bright yellow
        },
        background: {
            default: '#121212', // Main background color
            paper: '#1D1D1D',    // Cards, drawers, etc
        },
        text: {
            primary: '#ffffff',   // White text
            secondary: '#B0BEC5', // Greyish text
        },
        error: {
            main: '#f44336', // Red for errors
        }
    },
    typography: {
        fontFamily: "Rubik, serif", // Use your font
    },
})

export default homeTheme