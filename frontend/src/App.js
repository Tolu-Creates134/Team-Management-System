import Login from "./pages/Login/Login";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { ThemeProvider } from '@mui/material/styles';
import theme from './styles/theme';
import Register from "./pages/Register/Register";
import Home from "./pages/Home/Home";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Router>
        <Routes>
          <Route path='/' exact element={<Login/>} />
          <Route path='/register' exact element={<Register/>}/>
          <Route path='/home' exact element={<Home/>}/>
          
        </Routes>
      </Router>
    </ThemeProvider>

  );
}

export default App;
