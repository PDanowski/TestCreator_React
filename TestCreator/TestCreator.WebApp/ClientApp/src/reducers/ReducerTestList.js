import { GET_TEST_LIST } from "../actions/TestListActions";
const unloadedState = { testList: {}, isLoading: false, message: '' };
export const reducer = (state, incomingAction) => {
    const action = incomingAction;
    console.log(action.name);
    console.log(action.type);
    console.log('state');
    console.log(state);
    if (state === undefined) {
        return unloadedState;
    }
    switch (action.name) {
        case `${GET_TEST_LIST}_PENDING`:
            return Object.assign({}, state);
        case `${GET_TEST_LIST}_RECEIVE`:
            if (state.testList === undefined) {
                return Object.assign({}, state);
            }
            console.log(Object.assign(Object.assign({}, state.testList), { [action.type]: action.tests }));
            return Object.assign(Object.assign({}, state), { testList: Object.assign(Object.assign({}, state.testList), { [action.type]: action.tests }) });
        default:
            return state;
    }
};
//# sourceMappingURL=ReducerTestList.js.map