"use client";

import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import AppBar from '@mui/material/AppBar';
import CssBaseline from '@mui/material/CssBaseline';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import SettingsIcon from '@mui/icons-material/Settings';
import Link from 'next/link';

import { ApiProvider } from '@reduxjs/toolkit/dist/query/react';
import { api } from './api/baseApi';

const drawerWidth = 240;

const RootLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <html lang="en">
      <body>
        <main>
          <h1>Hello, Next.js!</h1>
          <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
              <Toolbar>
                <Typography variant="h6" noWrap component="div">
                  Clipped drawer
                </Typography>
              </Toolbar>
            </AppBar>
            <Drawer
              variant="permanent"
              sx={{
                width: drawerWidth,
                flexShrink: 0,
                [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
              }}
            >
              <Toolbar />
              <Box sx={{ overflow: 'auto' }}>
                <List>
                  {[
                    { Text: 'Settings', Route: '/settings' },
                    { Text: 'Leagues', Route: '/leagues' },
                    { Text: 'Tournaments', Route: '/tournaments' },
                  ]
                    .map((info, index) => (
                      <ListItem key={info.Text} disablePadding>
                        <ListItemButton>
                          <Link href={info.Route} style={{ display: 'flex', flexDirection: 'row', justifyContent: 'center', alignItems: 'center' }}>
                            <ListItemIcon>
                              {(() => {
                                switch (info.Text) {
                                  case 'Settings':
                                    return <SettingsIcon />
                                  case 'Leagues':
                                    return <SettingsIcon />
                                  case 'Tournaments':
                                    return <SettingsIcon />
                                }
                              })()}
                            </ListItemIcon>
                            <ListItemText primary={info.Text} />
                          </Link>
                        </ListItemButton>
                      </ListItem>
                    ))}
                </List>
              </Box>
            </Drawer>
            <ApiProvider api={api}>
            {children}
            </ApiProvider>
          </Box>
        </main>
      </body>
    </html>
  )
}

export default RootLayout;