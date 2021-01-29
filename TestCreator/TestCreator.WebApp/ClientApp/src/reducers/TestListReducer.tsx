import { GET_TEST_LIST, TestListAction } from "../actions/TestListActions";
import { Test } from "../interfaces/Test";
import { Action, Reducer } from 'redux';

export interface TestListState {
    isLoading: boolean;
    isFaulted: boolean;
    message: '';
    testLists: { [type: string]: Test[] };
    searchResultTests: Test[];
}


const unloadedState: TestListState = { testLists: {}, isLoading: false, message: '', isFaulted: false, searchResultTests: [] };


export const reducer: Reducer<TestListState> = (state: TestListState | undefined, incomingAction: Action): TestListState => {

    const action = incomingAction as TestListAction;

    console.log(action.type);
    console.log(state);

    if (state === undefined) {
        return unloadedState;
    }

    switch (action.type) {
        case `GET_TEST_LIST_PENDING`:
            return {
                ...state,
                isLoading: true,
                isFaulted: false
            }

        case `GET_TEST_LIST_RECEIVED`:
                console.log(action.listType);
                if (state.testLists === undefined) {
                return {
                    ...state,
                    isLoading: false,
                    isFaulted: true
                };
            }

            return {
                ...state,
                testLists: { ...state.testLists, [action.listType]: action.tests },
                isLoading: false,
                isFaulted: false
            }

        case `GET_TEST_LIST_ERROR`:
            return {
                ...state,
                isLoading: false,
                isFaulted: true
            }
        case `SEARCH_TEST_RESULT_PENDING`:
            return {
                ...state,
                isLoading: true,
                isFaulted: false
            }

        case `SEARCH_TEST_RESULT_RECEIVED`:
            return {
                ...state,
                searchResultTests: action.tests,
                isLoading: false,
                isFaulted: false
            }

        case `SEARCH_TEST_RESULT_ERROR`:
            return {
                ...state,
                isLoading: false,
                isFaulted: true
            }

    default:
        return state;
    }
}