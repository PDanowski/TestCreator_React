import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import './TestAttempt.css';
import { TestAttemptState } from "../../reducers/TestAttemptReducer";
import { testAttemptActionCreators } from "../../actions/TestAttemptActions";
import { ApplicationState } from '../../store';
import { TestAttemptEntry } from "../../interfaces/TestAttemptEntry";
import CircularProgress from '@material-ui/core/CircularProgress';
import { Button, TextField, Modal } from '@material-ui/core';

type StateProps = {
    testAttemptState: TestAttemptState | undefined;
}
type ComponentProps = {
    testId: number;
}
type DispatchProps = typeof testAttemptActionCreators

type TestAttemptComponentState = {
    testAttemptCurrentEntryIdx: number;
}

type TestAttemptProps = StateProps & DispatchProps & ComponentProps;

class TestAttempt extends React.Component<TestAttemptProps, TestAttemptComponentState> {
    constructor(props: TestAttemptProps) {
        super(props);
        this.state = {
            testAttemptCurrentEntryIdx: 0
        }

        this.props.getTestAttempt(this.props.testId);
    }

    shouldComponentUpdate(nextProps: TestAttemptProps, nextState: TestAttemptComponentState) {

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

        if (newTestAttemptState.isLoading !== this.props.testAttemptState?.isLoading) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }

        var newTestAttempt = newTestAttemptState.testAttempt;
        var oldTestAttempt = this.props.testAttemptState?.testAttempt;

        if (newTestAttempt === undefined) {
            console.log('shouldComponentUpdate newTestAttempt undefined');
            return false;
        }

        if (newTestAttempt.TestId !== oldTestAttempt?.TestId) {
            console.log('shouldComponentUpdate different TestId - update');
            return true;
        }

        return false;
    }


    renderQuestionList(entry: TestAttemptEntry) {
        console.log('renderQuestionList');
        console.log(this.state.testAttemptCurrentEntryIdx);
        if (entry.Answers === undefined) {
            return (
                <tbody>
                   <tr>
                        <td>
                            Loading...<br />
                            <CircularProgress />
                        </td>
                    </tr>
                </tbody>
            );
        }

        return (
            <tbody>
                {entry.Answers.map((answer) => {
                    return <tr>
                        <td className="form-group"> {entry.IsMultitipleChoise ?
                            <label className="form-control">
                               <input type="checkbox" />
                                    {answer.Text}
                               </label> :
                                <label className="form-control">
                                    <input id={answer.Id} type="radio" />
                                    {answer.Text}
                                </label>
                        }
                        </td>
                    </tr>
                })
            }
            </tbody>);
    }

    onNext(entry: TestAttemptEntry) {
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
        if (this.props.testAttemptState === undefined || this.props.testAttemptState.testAttempt === undefined
            || Object.keys(this.props.testAttemptState.testAttempt).length == 0 || this.props.testAttemptState?.isLoading) {
            return (
                <div>
                    Loading...<br />
                    <CircularProgress />
                </div>

            );
        }
        console.log('render return');
        console.log(this.props.testAttemptState);

        var currentTestAttemptEntry =
            this.props.testAttemptState.testAttempt?.TestAttemptEntries[this.state.testAttemptCurrentEntryIdx];

        return (
          <React.Fragment>
              <div className="card">
                    <h3> Test: {this.props.testAttemptState.testAttempt.Title}</h3>

                    {this.props.testAttemptState.testAttempt.TestAttemptEntries?.length === 0 ? <div>
                        Test cannot be started because it has no questions defined.
                                                                                           </div> :
                        <div className="table-responsive">
                            <div>
                                {currentTestAttemptEntry?.Question?.Text}
                                <table className="table table-hover">
                                    {this.renderQuestionList(currentTestAttemptEntry)}

                                </table>
                                <div className="commands">
                                    <Button variant="contained" onClick={(e) => this.onPrevious()}>Previous</Button>
                                    <Button variant="contained" onClick={(e) => this.onNext(currentTestAttemptEntry)}>Next</Button>
                                    <button type="button" className="btn btn-dark" >
                                        Finish
                                    </button >
                                </div>
                            </div >
                        </div>}
                </div>
          </React.Fragment>
);
}
}

const mapStateToProps = (state: ApplicationState, ownProps: any) => ({
    testId: ownProps.match.params.testId,
    testAttemptState: state.testAttempt
});

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
            getTestAttempt: testAttemptActionCreators.getTestAttempt,
        },
        dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(TestAttempt);