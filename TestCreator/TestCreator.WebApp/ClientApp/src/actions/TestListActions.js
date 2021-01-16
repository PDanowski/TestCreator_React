import axios from "axios";
const API_URL = 'https://localhost:44361/';
const TEST_LIST_URL = "api/test";
export const GET_TEST_LIST = 'GET_TEST_LIST';
export const testListActionCreators = {
    getTestList: (type) => (dispatch, getState) => {
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
            axios.get(API_URL + TEST_LIST_URL, { params: { sorting: numType } })
                .then(data => {
                dispatch({ name: 'GET_TEST_LIST_RECEIVE', tests: data.data, type: type });
            });
        }
    }
};
//# sourceMappingURL=TestListActions.js.map