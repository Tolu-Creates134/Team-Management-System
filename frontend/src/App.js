import Login from "./pages/Login/Login";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { ThemeProvider } from '@mui/material/styles';
import theme from './styles/theme';
import Register from "./pages/Register/Register";
import Home from "./pages/Home/Home";
import RequireAuth from "./components/RequireAuth";
import LayOut from "./components/LayOut";
import homeTheme from "./styles/homeTheme";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Routes>
        <Route path="/" element={<LayOut/>}>

          {/* public routes  */}
          <Route path='/' exact element={<Login/>} />
          <Route path='/register' exact element={<Register/>}/>

          {/* protected routes */}
          <Route element={<RequireAuth/>}>
            <Route 
              path='/home' 
              element={
                <ThemeProvider theme={homeTheme}>
                  <Home/>
                </ThemeProvider>
              }
            />
          </Route>

          {/* create a catch all path 404 page */}
        </Route>
      </Routes>
    </ThemeProvider>

  );
}

export default App;
