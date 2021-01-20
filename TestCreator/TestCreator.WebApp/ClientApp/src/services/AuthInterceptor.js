import axios from "axios";
import { isLoggedIn, getAuth } from './AuthService';
export function configureAuthInterceptor() {
    axios.interceptors.request.use((config) => {
        var token = (isLoggedIn()) ? getAuth().token : null;
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    }, (error) => {
        return Promise.reject(error);
    });
}
//# sourceMappingURL=AuthInterceptor.js.map