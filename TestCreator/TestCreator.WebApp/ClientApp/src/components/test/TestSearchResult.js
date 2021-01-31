import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import './TestSearchResult.css';
import { testListActionCreators } from "../../actions/TestListActions";
import CircularProgress from '@material-ui/core/CircularProgress';
import FormatListBulletedIcon from '@material-ui/icons/FormatListBulleted';
import { Link } from 'react-router-dom';
class TestSearchResult extends React.Component {
    constructor(props) {
        super(props);
        this.props.searchTests(this.props.keyword);
    }
    componentDidUpdate(prevProps, prevState, snapshot) {
        if (prevProps.keyword !== this.props.keyword) {
            this.props.searchTests(this.props.keyword);
        }
    }
    shouldComponentUpdate(nextProps, nextState) {
        var _a, _b;
        if (nextProps.keyword !== this.props.keyword) {
            console.log('shouldComponentUpdate different keyword - update');
            return true;
        }
        var newTestListState = nextProps.testListState;
        if (newTestListState === undefined) {
            console.log('shouldComponentUpdate newTestList undefined');
            return false;
        }
        if (newTestListState.isLoading !== ((_a = this.props.testListState) === null || _a === void 0 ? void 0 : _a.isLoading)) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }
        var newTestList = newTestListState.searchResultTests;
        var oldTestList = (_b = this.props.testListState) === null || _b === void 0 ? void 0 : _b.searchResultTests;
        if (newTestList === undefined) {
            console.log('shouldComponentUpdate newTestList undefined');
            return false;
        }
        if (newTestList.length !== (oldTestList === null || oldTestList === void 0 ? void 0 : oldTestList.length)) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }
        newTestList.forEach(element => {
            if (!(oldTestList === null || oldTestList === void 0 ? void 0 : oldTestList.includes(element))) {
                console.log('shouldComponentUpdate new element');
                return true;
            }
        });
        return false;
    }
    renderTestList() {
        var _a;
        if (this.props.testListState === undefined || this.props.testListState.searchResultTests === undefined || ((_a = this.props.testListState) === null || _a === void 0 ? void 0 : _a.isLoading)) {
            return (React.createElement("div", null,
                "Loading...",
                React.createElement("br", null),
                React.createElement(CircularProgress, null)));
        }
        return this.props.testListState.searchResultTests.map((test) => {
            return (React.createElement(Link, { to: '/testAttempt/' + test.Id },
                React.createElement("li", { className: "list-group-item" },
                    React.createElement("img", { src: 'https://picsum.photos/id/' + test.Id + '/50/50/', alt: test.Id.toString(), className: "rounded-circle" }),
                    React.createElement("span", null, test.Title))));
        });
    }
    render() {
        var _a, _b;
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: "card {{class}}" },
                React.createElement("div", { className: "card-header" },
                    React.createElement("div", { className: "form-inline" },
                        React.createElement("div", { className: "form-group" },
                            React.createElement(FormatListBulletedIcon, { color: "primary" })),
                        React.createElement("div", { className: "form-group" },
                            React.createElement("h4", null,
                                "Search results for keyword: ",
                                this.props.keyword)))),
                React.createElement("div", { className: "card-body" }, ((_b = (_a = this.props.testListState) === null || _a === void 0 ? void 0 : _a.searchResultTests) === null || _b === void 0 ? void 0 : _b.length) === 0 ? React.createElement("h5", null, "No results") :
                    React.createElement("ul", { className: "list-group" }, this.renderTestList())))));
    }
}
const mapStateToProps = (state, ownProps) => ({
    keyword: ownProps.match.params.keyword,
    testListState: state.testList
});
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        searchTests: testListActionCreators.searchTests,
    }, dispatch);
};
export default connect(mapStateToProps, mapDispatchToProps)(TestSearchResult);
//# sourceMappingURL=TestSearchResult.js.map