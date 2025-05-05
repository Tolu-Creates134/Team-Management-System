import React, { useContext } from 'react';
import Drawer from '@mui/material/Drawer';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Divider from '@mui/material/Divider';
import {
  ListItem,
  Typography,
  ListItemText,
  ListItemButton,
  ListItemIcon,
} from '@mui/material';
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import { mainNavigation } from '../../constants/mainNavigation';
import HomeIcon from '@mui/icons-material/Home';
import GroupIcon from '@mui/icons-material/Group';
import QueryStatsIcon from '@mui/icons-material/QueryStats';
import SettingsIcon from '@mui/icons-material/Settings';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import AuthContext from '../../context/AuthProvider';

const drawerWidth = 240;

const iconMap = {
  Home: <HomeIcon />,
  Teams: <GroupIcon />,
  //Players: ,
  Fixtures: <SportsSoccerIcon />,
  Stats: <QueryStatsIcon />,
  Settings: <SettingsIcon />,
};

const SideBar = () => {
  const { auth } = useContext(AuthContext);
  console.log(auth);
  return (
    <Box>
      {/* Home page title */}
      <AppBar
        position='fixed'
        sx={{
          width: `calc(100% - ${drawerWidth}px)`,
          ml: `${drawerWidth}px`,
        }}
      >
        <Toolbar>
          <Typography variant='h6' noWrap component='div'>
            Welcome back, {auth.firstName} {auth.lastName} !
          </Typography>
        </Toolbar>
      </AppBar>

      <Toolbar />
      <Divider />

      {/* Side bar navigation */}
      <Drawer
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            boxSizing: 'border-box',
          },
        }}
        variant='permanent'
        anchor='left'
      >
        <Box sx={{ display: 'flex', justifyContent: 'center' }}>
          <Typography variant='h2'>TMS</Typography>
        </Box>

        <List>
          {mainNavigation.map((navItem, index) => (
            <ListItem key={index}>
              <ListItemButton>
                <ListItemIcon>{iconMap[navItem.icon]}</ListItemIcon>
                <ListItemText>{navItem.title}</ListItemText>
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Drawer>
    </Box>
  );
};

export default SideBar;
