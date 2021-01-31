import axios from "axios";
import { TestAttempt } from "../interfaces/TestAttempt";
import { TestAttemptResult } from "../interfaces/TestAttemptResult";
import { AppThunkAction } from '../store/index';

const TEST_ATTEMPT_URL = "api/testAttempt";

export const GET_TEST_ATTEMPT = 'GET_TEST_ATTEMPT';
export const CALCULATE_TEST_ATTEMPT_RESULT = 'CALCULATE_TEST_ATTEMPT_RESULT';


interface GetTestAttemptAction {
    type: 'GET_TEST_ATTEMPT_RECEIVED' | 'GET_TEST_ATTEMPT_ERROR' | 'GET_TEST_ATTEMPT_PENDING';
    testAttempt: TestAttempt;
}

interface CalculateTestAttemptResultAction {
    type: 'CALCULATE_TEST_ATTEMPT_RESULT_RECEIVED' | 'CALCULATE_TEST_ATTEMPT_RESULT_ERROR' | 'CALCULATE_TEST_ATTEMPT_RESULT_PENDING';
    testAttemptResult: TestAttemptResult;
}

export type TestAttemptAction = GetTestAttemptAction | CalculateTestAttemptResultAction;

export const testAttemptActionCreators = {
    getTestAttempt: (testId: number): AppThunkAction<GetTestAttemptAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState) {

            dispatch({ type: 'GET_TEST_ATTEMPT_PENDING', testAttempt: {} as TestAttempt});
            axios.get<TestAttempt>(TEST_ATTEMPT_URL + "/" + testId)
                .then(data => {
                    dispatch({ type: 'GET_TEST_ATTEMPT_RECEIVED', testAttempt: data.data });
                })
                .catch(err => {
                    console.log(err);
                    dispatch({ type: 'GET_TEST_ATTEMPT_ERROR', testAttempt: {} as TestAttempt });
                });
        }
    },
    calculateTestAttemptResult: (testAttempt: TestAttempt): AppThunkAction<CalculateTestAttemptResultAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState) {
            dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_PENDING', testAttemptResult: {} as TestAttemptResult });
            axios.post<TestAttemptResult>(TEST_ATTEMPT_URL, testAttempt)
                .then(data => {
                    dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_RECEIVED', testAttemptResult: data.data });
                })
                .catch(err => {
                    console.log(err);
                    dispatch({ type: 'CALCULATE_TEST_ATTEMPT_RESULT_ERROR', testAttemptResult: {} as TestAttemptResult });
                });
        }
    }
};