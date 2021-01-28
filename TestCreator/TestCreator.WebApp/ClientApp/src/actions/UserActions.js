import axios from "axios";
const REGISTER_URL = "api/user";
export const ADD_USER = 'ADD_USER';
export const userActionCreators = {
    addUser: (user) => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            axios.post(REGISTER_URL, user).then(() => {
                dispatch({ type: 'ADD_USER_RECEIVED', user: user });
            })
                .catch((err) => {
                console.log(err);
                dispatch({ type: 'ADD_USER_ERROR', user: {} });
            });
        }
    }
};
//# sourceMappingURL=UserActions.js.map