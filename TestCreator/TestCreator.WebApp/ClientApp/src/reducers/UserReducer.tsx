import { ADD_USER, UserAction } from "../actions/UserActions";
import { User } from "../interfaces/User";
import { Action, Reducer } from 'redux';

export interface UserState {
    isFaulted: boolean;
    isCreated: boolean;
    message: '';
    user: User;
}


const unloadedState: UserState = { user: {} as User, isFaulted: false, message: '', isCreated: false };

          export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {

    const action = incomingAction as UserAction;

    console.log(action.type);
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }

    switch (action.type) {

        case `ADD_USER_RECEIVED`:
            if (state.user === undefined) {
                return {
                    ...state,
                    isFaulted: true,
                    isCreated: false
                };
            }

        return {
            ...state,
            isFaulted: false,
            isCreated: true
        }

        case `ADD_USER_ERROR`:
        return {
            ...state,
            isFaulted: true,
            isCreated: false
        }

    default:
        return state;
    }
}