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
import { Link } from 'react-router-dom';
import { useLocation } from 'react-router-dom'

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: 'auto',
            flexGrow: 1,
        },
        title: {
            flexGrow: 1,
        },
        button : {
            marginLeft: theme.spacing(1),
            marginRight: theme.spacing(1)
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
    var [value, setValue] = React.useState(0);

    value = route;

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
                    <Button className={classes.button} color="inherit" startIcon={<InputIcon />}>Login</Button>
                    <Button className={classes.button} color="inherit" startIcon={<AssignmentIndIcon />}>Register</Button>
                    <Button className={classes.button} color="inherit" startIcon={<ExitToAppIcon />}>Logout</Button>
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
                        />
                    </div>
                </Toolbar>
            </AppBar>
        </div>
    );
}