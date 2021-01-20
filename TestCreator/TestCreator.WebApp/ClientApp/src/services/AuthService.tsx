import * as React from 'react';
import { Observable } from "rxjs";
import { map, catchError } from 'rxjs/operators';
import { TokenResponse } from "../interfaces/TokenResponse";
import axios from "axios";


const authKey: string = "auth";
const clientId: string = "TestCreator";

export function login(username: string, password: string): Promise<boolean> {
    var url = "api/token/auth";
    var data = {
        Username: username,
        Password: password,
        ClientId: clientId,
        GrantType: "password",
        Scope: "offline_access profile email"
    };

    return getAuthFromServer(url, data);
}

export function logout(): boolean {
    setAuth(null);
    return true;
}

export function getAuth(): TokenResponse | null {
    var item = localStorage.getItem(authKey);
    if (item) {
        return JSON.parse(item);
    }
    return null;
}

export function isLoggedIn(): boolean {
    return localStorage.getItem(authKey) != null;
}

function setAuth(auth: TokenResponse | null) {
    if (auth) {
        localStorage.setItem(authKey, JSON.stringify(auth));
    } else {
        localStorage.removeItem(authKey);
    }
}

function refreshToken(): Promise<boolean> {
    var url = "api/token/auth";
    var data = {
        ClientId: clientId,
        GrantType: "refresh_token",
        Scope: "offline_access profile email",
        RefreshToken: getAuth()!.refreshToken
    };

    return getAuthFromServer(url, data);
}

function getAuthFromServer(url: string, data: any): Promise<boolean> {
    return axios.post<TokenResponse>(url, data).then((result) => {
        let token = result.data && result.data.token;
  
        if (token) {
          setAuth(result.data);
  
          return true;
        }
  
        return false;
      })
    .catch((err) => { 
        console.log(err);
        return false; 
    });
}