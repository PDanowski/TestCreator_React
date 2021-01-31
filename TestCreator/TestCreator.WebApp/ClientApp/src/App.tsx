import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/home/Home';
import About from './components/about/About';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Login from './components/login/Login';
import Register from './components/register/Register';
import TestSearchResult from './components/test/TestSearchResult';
import TestAttempt from './components/test/TestAttempt';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/about' component={About} />
        <Route path='/counter' component={Counter} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Register} />
        <Route path='/search/:keyword' component={TestSearchResult} />
        <Route path='/testAttempt/:testId' component={TestAttempt} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
    </Layout>
);
