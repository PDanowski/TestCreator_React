import axios from "axios";
import { Test } from "../interfaces/Test";
import { AppThunkAction } from '../store/index';

const TEST_LIST_URL = "api/test";

export const GET_TEST_LIST = 'GET_TEST_LIST';
export const SEARCH_TEST_RESULT = 'SEARCH_TEST_RESULT';

interface GetTestListAction {
    type: 'GET_TEST_LIST_RECEIVED' | 'GET_TEST_LIST_ERROR' | 'GET_TEST_LIST_PENDING';
    tests: Test[];
    listType: string;
}

interface SearchTestsResultAction {
    type: 'SEARCH_TEST_RESULT_RECEIVED' | 'SEARCH_TEST_RESULT_ERROR' | 'SEARCH_TEST_RESULT_PENDING';
    tests: Test[];
}

export type TestListAction = GetTestListAction | SearchTestsResultAction;

export const testListActionCreators = {
    getTestList: (type: string): AppThunkAction<GetTestListAction> => (dispatch, getState) => {

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
    },
    searchTests: (keyword: string): AppThunkAction<SearchTestsResultAction> => (dispatch, getState) => {

        const appState = getState();
        if (appState) {
            dispatch({ type: 'SEARCH_TEST_RESULT_PENDING', tests: []});
            axios.get<Test[]>(TEST_LIST_URL, { params: { keyword: keyword } })
                .then(data => {
                    dispatch({ type: 'SEARCH_TEST_RESULT_RECEIVED', tests: data.data});
                })
                .catch(err => {
                    console.log(err);
                    dispatch({ type: 'SEARCH_TEST_RESULT_ERROR', tests: []});
                });
        }
    }
};