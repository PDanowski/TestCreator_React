const unloadedState = { testLists: {}, isLoading: false, message: '', isFaulted: false, searchResultTests: [] };
export const reducer = (state, incomingAction) => {
    const action = incomingAction;
    console.log(action.type);
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }
    switch (action.type) {
        case `GET_TEST_LIST_PENDING`:
            return Object.assign(Object.assign({}, state), { isLoading: true, isFaulted: false });
        case `GET_TEST_LIST_RECEIVED`:
            console.log(action.listType);
            if (state.testLists === undefined) {
                return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
            }
            return Object.assign(Object.assign({}, state), { testLists: Object.assign(Object.assign({}, state.testLists), { [action.listType]: action.tests }), isLoading: false, isFaulted: false });
        case `GET_TEST_LIST_ERROR`:
            return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
        case `SEARCH_TEST_RESULT_PENDING`:
            return Object.assign(Object.assign({}, state), { isLoading: true, isFaulted: false });
        case `SEARCH_TEST_RESULT_RECEIVED`:
            return Object.assign(Object.assign({}, state), { searchResultTests: action.tests, isLoading: false, isFaulted: false });
        case `SEARCH_TEST_RESULT_ERROR`:
            return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
        default:
            return state;
    }
};
//# sourceMappingURL=TestListReducer.js.map