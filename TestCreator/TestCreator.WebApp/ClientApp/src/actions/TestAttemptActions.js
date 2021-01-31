import axios from "axios";
const TEST_ATTEMPT_URL = "api/testAttempt";
export const GET_TEST_ATTEMPT = 'GET_TEST_ATTEMPT';
export const CALCULATE_TEST_ATTEMPT_RESULT = 'CALCULATE_TEST_ATTEMPT_RESULT';
export const testAttemptActionCreators = {
    getTestAttempt: (testId) => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            dispatch({ type: 'GET_TEST_ATTEMPT_PENDING', testAttempt: {} });
            axios.get(TEST_ATTEMPT_URL + "/" + testId)
                .then(data => {
                dispatch({ type: 'GET_TEST_ATTEMPT_RECEIVED', testAttempt: data.data });
            })
                .catch(err => {
                console.log(err);
                dispatch({ type: 'GET_TEST_ATTEMPT_ERROR', testAttempt: {} });
            });
        }
    },
    calculateTestAttemptResult: (testAttempt) => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_PENDING', testAttemptResult: {} });
            axios.post(TEST_ATTEMPT_URL, testAttempt)
                .then(data => {
                dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_RECEIVED', testAttemptResult: data.data });
            })
                .catch(err => {
                console.log(err);
                dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_ERROR', testAttemptResult: {} });
            });
        }
    }
};
//# sourceMappingURL=TestAttemptActions.js.map