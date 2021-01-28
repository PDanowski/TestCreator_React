import { ADD_USER } from "../actions/UserActions";
const unloadedState = { user: {}, isFaulted: false, message: '', isCreated: false };
export const reducer = (state, incomingAction) => {
    const action = incomingAction;
    console.log(action.type);
    console.log('state');
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }
    switch (action.type) {
        case `${ADD_USER}_RECEIVED`:
            if (state.user === undefined) {
                return Object.assign(Object.assign({}, state), { isFaulted: true, isCreated: false });
            }
            console.log(Object.assign({}, state.user));
            return Object.assign(Object.assign({}, state), { isFaulted: false, isCreated: true });
        case `${ADD_USER}_ERROR`:
            return Object.assign(Object.assign({}, state), { isFaulted: true, isCreated: false });
        default:
            return state;
    }
};
//# sourceMappingURL=UserReducer.js.map