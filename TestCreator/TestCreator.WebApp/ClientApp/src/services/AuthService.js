import axios from "axios";
const authKey = "auth";
const clientId = "TestCreator";
export function login(username, password) {
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
export function logout() {
    setAuth(null);
    return true;
}
export function getAuth() {
    var item = localStorage.getItem(authKey);
    if (item) {
        return JSON.parse(item);
    }
    return null;
}
export function isLoggedIn() {
    return localStorage.getItem(authKey) != null;
}
function setAuth(auth) {
    if (auth) {
        localStorage.setItem(authKey, JSON.stringify(auth));
    }
    else {
        localStorage.removeItem(authKey);
    }
}
function refreshToken() {
    var url = "api/token/auth";
    var data = {
        ClientId: clientId,
        GrantType: "refresh_token",
        Scope: "offline_access profile email",
        RefreshToken: getAuth().refreshToken
    };
    return getAuthFromServer(url, data);
}
function getAuthFromServer(url, data) {
    return axios.post(url, data).then((result) => {
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
//# sourceMappingURL=AuthService.js.map