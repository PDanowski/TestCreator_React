import axios from "axios";
import * as React from 'react';
import { isLoggedIn, getAuth } from './AuthService';

export function configureAuthInterceptor() {
    axios.interceptors.request.use((config) => {
        var token = (isLoggedIn()) ? getAuth()!.token : null;

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    }, (error) => {
        return Promise.reject(error);
    });
}