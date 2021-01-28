import { GET_TEST_LIST } from "../actions/TestListActions";
const unloadedState = { testList: {}, isLoading: false, message: '', isFaulted: false };
export const reducer = (state, incomingAction) => {
    const action = incomingAction;
    console.log(action.listType);
    console.log(action.type);
    console.log('state');
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }
    switch (action.type) {
        case `${GET_TEST_LIST}_PENDING`:
            return Object.assign(Object.assign({}, state), { isLoading: true, isFaulted: false });
        case `${GET_TEST_LIST}_RECEIVED`:
            if (state.testList === undefined) {
                return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
            }
            console.log(Object.assign(Object.assign({}, state.testList), { [action.listType]: action.tests }));
            return Object.assign(Object.assign({}, state), { testList: Object.assign(Object.assign({}, state.testList), { [action.listType]: action.tests }), isLoading: false, isFaulted: false });
        case `${GET_TEST_LIST}_ERROR`:
            return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
        default:
            return state;
    }
};
//# sourceMappingURL=TestListReducer.js.map