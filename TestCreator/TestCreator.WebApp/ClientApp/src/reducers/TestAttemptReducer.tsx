import { Action, Reducer } from 'redux';
import { TestAttempt } from "../interfaces/TestAttempt";
import { TestAttemptResult } from "../interfaces/TestAttemptResult";
import { TestAttemptAction } from "../actions/TestAttemptActions";

export interface TestAttemptState {
    isLoading: boolean;
    isFaulted: boolean;
    message: '';
    testAttempt: TestAttempt;
    testAttemptResult: TestAttemptResult;
}

const unloadedState: TestAttemptState = { testAttempt: {} as TestAttempt, isLoading: false, message: '', isFaulted: false, testAttemptResult: {} as TestAttemptResult };

export const reducer: Reducer<TestAttemptState> = (state: TestAttemptState | undefined, incomingAction: Action): TestAttemptState => {

    const action = incomingAction as TestAttemptAction;

    console.log(action.type);
    console.log(state);

    if (state === undefined) {
        return unloadedState;
    }

    switch (action.type) {
        case `GET_TEST_ATTEMPT_PENDING`:
            return {
                ...state,
                isLoading: true,
                isFaulted: false
            }

        case `GET_TEST_ATTEMPT_RECEIVED`:

            return {
                ...state,
                testAttempt: action.testAttempt,
                isLoading: false,
                isFaulted: false
            }

        case `GET_TEST_ATTEMPT_ERROR`:
            return {
                ...state,
                isLoading: false,
                isFaulted: true
            }
        case `CALCULATE_TEST_ATTEMPT_RESULT_PENDING`:
            return {
                ...state,
                isLoading: true,
                isFaulted: false
            }

        case `CALCULATE_TEST_ATTEMPT_RESULT_RECEIVED`:
            return {
                ...state,
                testAttemptResult: action.testAttemptResult,
                isLoading: false,
                isFaulted: false
            }

        case `CALCULATE_TEST_ATTEMPT_RESULT_ERROR`:
            return {
                ...state,
                isLoading: false,
                isFaulted: true
            }

        default:
            return state;
    }
}