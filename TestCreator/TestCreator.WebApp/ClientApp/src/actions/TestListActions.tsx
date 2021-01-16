﻿import axios from "axios"
import { Test } from "../interfaces/Test";
import { AppThunkAction } from '../store/index';

const API_URL = 'https://localhost:44361/';
const TEST_LIST_URL = "api/test";

export const GET_TEST_LIST = 'GET_TEST_LIST';

interface RequestTestListAction {
    name: 'GET_TEST_LIST_PENDING';
    tests: Test[];
    type: string;
}

interface ReceiveTestListAction {
    name: 'GET_TEST_LIST_RECEIVE';
    tests: Test[];
    type: string;
}

export type TestListAction = RequestTestListAction | ReceiveTestListAction;

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

            axios.get<Test[]>(API_URL + TEST_LIST_URL, { params: { sorting: numType } })
                .then(data => {
                    dispatch({ name: 'GET_TEST_LIST_RECEIVE', tests: data.data, type: type });
                });
        }
    }
};