import axios from "axios";
import { User } from "../interfaces/User";
import { AppThunkAction } from '../store/index';

const REGISTER_URL = "api/user";

export const ADD_USER = 'ADD_USER';

interface AddUserAction {
    type: 'ADD_USER_RECEIVED' | 'ADD_USER_ERROR';
    user: User;
}


export type UserAction = AddUserAction;

export const userActionCreators = {
    addUser: (user: User): AppThunkAction<UserAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState) {

            axios.post<User>(REGISTER_URL, user).then(() => {
                dispatch({ type: 'ADD_USER_RECEIVED', user: user});
                })
                .catch((err) => {
                    console.log(err);
                    dispatch({ type: 'ADD_USER_ERROR', user: {} as User });
                });
        }
    }
};