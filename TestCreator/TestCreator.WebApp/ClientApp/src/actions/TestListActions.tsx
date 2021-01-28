import axios from "axios"
import { Test } from "../interfaces/Test";
import { AppThunkAction } from '../store/index';

const TEST_LIST_URL = "api/test";

export const GET_TEST_LIST = 'GET_TEST_LIST';

interface PendingTestListAction {
    type: 'GET_TEST_LIST_PENDING';
    tests: Test[];
    listType: string;
}

interface ReceivedTestListAction {
    type: 'GET_TEST_LIST_RECEIVED';
    tests: Test[];
    listType: string;
}

interface ErrorTestListAction {
    type: 'GET_TEST_LIST_ERROR';
    tests: Test[];
    listType: string;
}

export type TestListAction = PendingTestListAction | ReceivedTestListAction | ErrorTestListAction;

export const testListActionCreators = {
    getTestList: (type: string): AppThunkAction<TestListAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState) {

            var numType = 0;

            switch (type) {
            case "random":
                numType = 0;
                break;
            case "latest":
                numType = 1;
                break;
            case "byTitle":
                numType = 2;
                break;
            }

            dispatch({ type: 'GET_TEST_LIST_PENDING', tests: [], listType: type });
            axios.get<Test[]>(TEST_LIST_URL, { params: { sorting: numType } })
                .then(data => {
                    dispatch({ type: 'GET_TEST_LIST_RECEIVED', tests: data.data, listType: type });
                })
                .catch(err => {
                    console.log(err);
                    dispatch({ type: 'GET_TEST_LIST_ERROR', tests: [], listType: type });
                });
        }
    }
};