import { GET_TEST_LIST, TestListAction } from "../actions/TestListActions";
import { Test } from "../interfaces/Test";
import { Action, Reducer } from 'redux';

export interface TestListState {
    isLoading: boolean;
    isFaulted: boolean;
    message: '';
    testList: { [type: string]: Test[] };
}


const unloadedState: TestListState = { testList: {}, isLoading: false, message: '', isFaulted: false };


export const reducer: Reducer<TestListState> = (state: TestListState | undefined, incomingAction: Action): TestListState => {

    const action = incomingAction as TestListAction;

    console.log(action.listType);
    console.log(action.type);
    console.log('state');
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }

    switch (action.type) {
    case `${GET_TEST_LIST}_PENDING`:
        return {
            ...state,
            isLoading: true,
            isFaulted: false
        }

    case `${GET_TEST_LIST}_RECEIVED`:
        if (state.testList === undefined) {
            return {
                ...state,
                isLoading: false,
                isFaulted: true
            };
        }

            console.log({ ...state.testList, [action.listType]: action.tests })
        return {
            ...state,
            testList: { ...state.testList, [action.listType]: action.tests },
            isLoading: false,
            isFaulted: false
        }

    case `${GET_TEST_LIST}_ERROR`:
        return {
            ...state,
            isLoading: false,
            isFaulted: true
        }

    default:
        return state;
    }
}