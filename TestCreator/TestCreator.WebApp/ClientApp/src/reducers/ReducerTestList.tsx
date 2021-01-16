import { GET_TEST_LIST, TestListAction } from "../actions/TestListActions";
import { Test } from "../interfaces/Test";
import { Action, Reducer } from 'redux';

export interface TestListState {
    isLoading: boolean;
    message: '';
    testList: { [type: string]: Test[] };
}


const unloadedState: TestListState = { testList: {}, isLoading: false, message: ''};


export const reducer: Reducer<TestListState> = (state: TestListState | undefined, incomingAction: Action): TestListState => {

    const action = incomingAction as TestListAction;

    console.log(action.name);
    console.log(action.type);
    console.log('state');
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }

    switch (action.name) {
        case `${GET_TEST_LIST}_PENDING`:
            return { ...state }
        case `${GET_TEST_LIST}_RECEIVE`:
            if (state.testList === undefined) {
                return {
                    ...state,
                };
            }

            console.log({ ...state.testList, [action.type]: action.tests })
            return {
                ...state,
                testList: { ...state.testList, [action.type]: action.tests },
            }
        default:
            return state;
    }
}