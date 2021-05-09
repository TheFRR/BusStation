import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import AccountCircle from '@material-ui/icons/AccountCircle';
import ShoppingBasketIcon from '@material-ui/icons/ShoppingBasket';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import { useHistory } from "react-router-dom";
import { getUser } from '../API/Auth';
import { signout } from '../API/Auth'

import "../custom.css"

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));

export default function MenuAppBar() {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState(null);
  
  const open = Boolean(anchorEl);

  const [user, setUser] = React.useState(null);

  const _setUser = async () => {
    const response = await getUser();
    if (response.ok) {
      let body = await response.json();
      setUser(body);
    }
    else setUser(null);
  };

  useEffect(() => {
    _setUser();
  }, []);

  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const history = useHistory();

  const navigateToLogin = () => {
    history.push('/login');
    setAnchorEl(null);
  }

  const navigateToRegistartion = () => {
    history.push('/register');
    setAnchorEl(null);
  }

  const navigateToUpdate = () => {
    history.push('/route');
    setAnchorEl(null);
  }

  const navigateToMain = () => {
    history.push('/');
  }

  const logOut = () => {
    signout();
    handleClose();
    navigateToMain();  
  }

  return (
    <div className={classes.root}>
      <AppBar id="bar" position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title} onClick={navigateToMain}>
            <a id="href" href="#" color="white">Автовокзал</a>
          </Typography>
          {(
            <div>
              <IconButton
                aria-label="basket of current user"
                aria-haspopup="true"
                color="inherit"
              >
                <ShoppingBasketIcon />
              </IconButton>
              <IconButton
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={handleMenu}
                color="inherit"
              >
                <AccountCircle />
              </IconButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={open}
                onClose={handleClose}
              >
              { _setUser() && user === null &&
                <MenuItem onClick={navigateToLogin}>Вход</MenuItem>
              }
              { user === null &&
                <MenuItem onClick={navigateToRegistartion}>Регистрация</MenuItem>  
              }          
              { user !== null && user.userName === "admin@mail.com" &&
                <MenuItem onClick={navigateToUpdate}>Редактирование расписания</MenuItem>
              }
              { user !== null &&
                <MenuItem onClick={logOut}>Выход из аккаунта</MenuItem>
              }
              </Menu>
            </div>
          )}
        </Toolbar>
      </AppBar>
    </div>
  );
}