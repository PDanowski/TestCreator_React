const unloadedState = { testAttempt: {}, isLoading: false, message: '', isFaulted: false, testAttemptResult: {} };
export const reducer = (state, incomingAction) => {
    const action = incomingAction;
    console.log(action.type);
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }
    switch (action.type) {
        case `GET_TEST_ATTEMPT_PENDING`:
            return Object.assign(Object.assign({}, state), { isLoading: true, isFaulted: false });
        case `GET_TEST_ATTEMPT_RECEIVED`:
            return Object.assign(Object.assign({}, state), { testAttempt: action.testAttempt, isLoading: false, isFaulted: false });
        case `GET_TEST_ATTEMPT_ERROR`:
            return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
        case `CALCULATE_TEST_ATTEMPT_RESULT_PENDING`:
            return Object.assign(Object.assign({}, state), { isLoading: true, isFaulted: false });
        case `CALCULATE_TEST_ATTEMPT_RESULT_RECEIVED`:
            return Object.assign(Object.assign({}, state), { testAttemptResult: action.testAttemptResult, isLoading: false, isFaulted: false });
        case `CALCULATE_TEST_ATTEMPT_RESULT_ERROR`:
            return Object.assign(Object.assign({}, state), { isLoading: false, isFaulted: true });
        default:
            return state;
    }
};
//# sourceMappingURL=TestAttemptReducer.js.map