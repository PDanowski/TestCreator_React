import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import './TestAttempt.css';
import { testAttemptActionCreators } from "../../actions/TestAttemptActions";
import CircularProgress from '@material-ui/core/CircularProgress';
import { Button } from '@material-ui/core';
class TestAttempt extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            testAttemptCurrentEntryIdx: 0
        };
        this.props.getTestAttempt(this.props.testId);
    }
    shouldComponentUpdate(nextProps, nextState) {
        var _a, _b;
        if (nextProps.testId !== this.props.testId) {
            console.log('shouldComponentUpdate different testId - update');
            return true;
        }
        var newTestAttemptState = nextProps.testAttemptState;
        if (newTestAttemptState === undefined) {
            console.log('shouldComponentUpdate testAttemptState undefined');
            return false;
        }
        var newComponentState = nextState.testAttemptCurrentEntryIdx;
        if (newComponentState !== this.state.testAttemptCurrentEntryIdx) {
            console.log('shouldComponentUpdate testAttemptCurrentEntryIdx - update');
            return true;
        }
        if (newTestAttemptState.isLoading !== ((_a = this.props.testAttemptState) === null || _a === void 0 ? void 0 : _a.isLoading)) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }
        var newTestAttempt = newTestAttemptState.testAttempt;
        var oldTestAttempt = (_b = this.props.testAttemptState) === null || _b === void 0 ? void 0 : _b.testAttempt;
        if (newTestAttempt === undefined) {
            console.log('shouldComponentUpdate newTestAttempt undefined');
            return false;
        }
        if (newTestAttempt.TestId !== (oldTestAttempt === null || oldTestAttempt === void 0 ? void 0 : oldTestAttempt.TestId)) {
            console.log('shouldComponentUpdate different TestId - update');
            return true;
        }
        return false;
    }
    renderQuestionList(entry) {
        console.log('renderQuestionList');
        console.log(this.state.testAttemptCurrentEntryIdx);
        if (entry.Answers === undefined) {
            return (React.createElement("tbody", null,
                React.createElement("tr", null,
                    React.createElement("td", null,
                        "Loading...",
                        React.createElement("br", null),
                        React.createElement(CircularProgress, null)))));
        }
        return (React.createElement("tbody", null, entry.Answers.map((answer) => {
            return React.createElement("tr", null,
                React.createElement("td", { className: "form-group" },
                    " ",
                    entry.IsMultitipleChoise ?
                        React.createElement("label", { className: "form-control" },
                            React.createElement("input", { type: "checkbox" }),
                            answer.Text) :
                        React.createElement("label", { className: "form-control" },
                            React.createElement("input", { id: answer.Id, type: "radio" }),
                            answer.Text)));
        })));
    }
    onNext(entry) {
        var value = this.state.testAttemptCurrentEntryIdx + 1;
        if (value < entry.Answers.length) {
            this.setState({
                testAttemptCurrentEntryIdx: value
            });
        }
    }
    onPrevious() {
        var value = this.state.testAttemptCurrentEntryIdx - 1;
        if (value >= 0) {
            this.setState({
                testAttemptCurrentEntryIdx: value
            });
        }
    }
    onSubmit() {
        //this.props.calculateTestAttemptResult();
    }
    render() {
        var _a, _b, _c, _d;
        if (this.props.testAttemptState === undefined || this.props.testAttemptState.testAttempt === undefined
            || Object.keys(this.props.testAttemptState.testAttempt).length == 0 || ((_a = this.props.testAttemptState) === null || _a === void 0 ? void 0 : _a.isLoading)) {
            return (React.createElement("div", null,
                "Loading...",
                React.createElement("br", null),
                React.createElement(CircularProgress, null)));
        }
        console.log('render return');
        console.log(this.props.testAttemptState);
        var currentTestAttemptEntry = (_b = this.props.testAttemptState.testAttempt) === null || _b === void 0 ? void 0 : _b.TestAttemptEntries[this.state.testAttemptCurrentEntryIdx];
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: "card" },
                React.createElement("h3", null,
                    " Test: ",
                    this.props.testAttemptState.testAttempt.Title),
                ((_c = this.props.testAttemptState.testAttempt.TestAttemptEntries) === null || _c === void 0 ? void 0 : _c.length) === 0 ? React.createElement("div", null, "Test cannot be started because it has no questions defined.") :
                    React.createElement("div", { className: "table-responsive" },
                        React.createElement("div", null, (_d = currentTestAttemptEntry === null || currentTestAttemptEntry === void 0 ? void 0 : currentTestAttemptEntry.Question) === null || _d === void 0 ? void 0 :
                            _d.Text,
                            React.createElement("table", { className: "table table-hover" }, this.renderQuestionList(currentTestAttemptEntry)),
                            React.createElement("div", { className: "commands" },
                                React.createElement(Button, { variant: "contained", onClick: (e) => this.onPrevious() }, "Previous"),
                                React.createElement(Button, { variant: "contained", onClick: (e) => this.onNext(currentTestAttemptEntry) }, "Next"),
                                React.createElement("button", { type: "button", className: "btn btn-dark" }, "Finish")))))));
    }
}
const mapStateToProps = (state, ownProps) => ({
    testId: ownProps.match.params.testId,
    testAttemptState: state.testAttempt
});
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        getTestAttempt: testAttemptActionCreators.getTestAttempt,
    }, dispatch);
};
export default connect(mapStateToProps, mapDispatchToProps)(TestAttempt);
//# sourceMappingURL=TestAttempt.js.map