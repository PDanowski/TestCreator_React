import * as React from 'react';
import { BottomNavigation, BottomNavigationAction, AppBar, Toolbar, Button, InputBase, Typography } from '@material-ui/core';
import InfoIcon from '@material-ui/icons/Info';
import AddIcon from '@material-ui/icons/Add';
import HomeIcon from '@material-ui/icons/Home';
import SearchIcon from '@material-ui/icons/Search';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import InputIcon from '@material-ui/icons/Input';
import { createStyles, fade, Theme, makeStyles } from '@material-ui/core/styles';
import { Link, useHistory, useLocation, Redirect  } from 'react-router-dom';
import { isLoggedIn, logout } from '../../services/AuthService';
import { blue } from '@material-ui/core/colors';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: 'auto',
            marginBottom: '15px',
            flexGrow: 1,
        },
        title: {
            flexGrow: 1,
        },
        button : {
            marginLeft: theme.spacing(1),
            marginRight: theme.spacing(1),
            color: theme.palette.getContrastText(blue[500]),
            backgroundColor: blue[500],
            borderColor: '#fafafa',
            '&:hover': {
                backgroundColor: '#fafafa'
            },
        },
        navbar: {
            position: 'relative',
            flexGrow: 1,
            marginLeft: theme.spacing(2),
            marginRight: theme.spacing(2),
            width: '20%',
        },
        search: {
            position: 'relative',
            borderRadius: theme.shape.borderRadius,
            backgroundColor: fade(theme.palette.common.white, 0.15),
            '&:hover': {
                backgroundColor: fade(theme.palette.common.white, 0.25),
            },
            marginLeft: 0,
            width: '100%',
            [theme.breakpoints.up('sm')]: {
                marginLeft: theme.spacing(1),
                width: 'auto',
            },
        },
        searchIcon: {
            padding: theme.spacing(0, 2),
            height: '100%',
            position: 'absolute',
            pointerEvents: 'none',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
        },
        inputRoot: {
            color: 'inherit',
        },
        inputInput: {
            padding: theme.spacing(1, 1, 1, 0),
            // vertical padding + font size from searchIcon
            paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
            transition: theme.transitions.create('width'),
            width: '100%',
            [theme.breakpoints.up('sm')]: {
                width: '12ch',
                '&:focus': {
                    width: '20ch',
                },
            },
        },
    }),
);


export default function NavMenu() {
    var location = useLocation();
    var route = location.pathname.substring(1, location.pathname.length);

    const classes = useStyles();
    var [value, setValue] = React.useState('');
    const isLogged = isLoggedIn();
    const history = useHistory();
    const [keyword, setKeyword] = React.useState('');

    value = route;

    function handleLogout(){
        if (logout()) {
            history.go(0);
            //history.push('/');
        }
        return false;
    }

    function handleSearch() {
        const url = `/search/${keyword}`;
        history.push(url);
    }

    return (

        <div className={classes.root}>
            <AppBar position="static">
                <Toolbar>
                    <Typography variant="h5" className={classes.title}>
                        Test creator
                    </Typography>
                    <BottomNavigation
                        value={value}
                        onChange={(event, newValue) => {
                            setValue(newValue);
                        }}
                        showLabels
                        className={classes.navbar}>
                        <BottomNavigationAction label="Home" icon={<HomeIcon />}
                                                component={Link}
                                                to="/"
                                                value="" />
                        <BottomNavigationAction label="About" icon={<InfoIcon />}
                                                component={Link}
                                                to="/about"
                                                value="about"/>
                        <BottomNavigationAction label="Create test" icon={<AddIcon />} />
                    </BottomNavigation>
                    {!isLogged ? <Button className={classes.button} variant="outlined" startIcon={<InputIcon />} component={Link} to="/login">Login</Button> : null}
                    {!isLogged ? <Button className={classes.button} variant="outlined" startIcon={<AssignmentIndIcon />} component={Link} to="/register">Register</Button> : null}
                    {isLogged ? <Button className={classes.button} variant="outlined" startIcon={<ExitToAppIcon />} onClick={() => handleLogout()}>Logout</Button> : null}
                    <div className={classes.search}>
                        <div className={classes.searchIcon}>
                            <SearchIcon />
                        </div>
                        <InputBase
                            placeholder="Search..."
                            classes={{
                                root: classes.inputRoot,
                                input: classes.inputInput,
                            }}
                            inputProps={{ 'aria-label': 'search' }}
                            value={keyword}
                            onChange={e => setKeyword((e.target as HTMLInputElement).value)}
                            onKeyPress={e => {
                                if (e.key === 'Enter') {
                                    handleSearch();
                                }
                            }}
                        />
                    </div>
                </Toolbar>
            </AppBar>
        </div>
    );
}