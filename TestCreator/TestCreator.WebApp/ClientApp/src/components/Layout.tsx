import * as React from 'react';
import { Box  } from '@material-ui/core';
import NavMenu from './nav-menu/NavMenu';
import './Layout.css';

export default (props: { children?: React.ReactNode }) => (
    <div>
        <React.Fragment>
            <NavMenu />
            <Box>
                {props.children}
            </Box >
        </React.Fragment>
    </div>
);
