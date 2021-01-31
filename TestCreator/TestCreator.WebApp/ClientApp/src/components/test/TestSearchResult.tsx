import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import './TestSearchResult.css';
import { testListActionCreators } from "../../actions/TestListActions";
import { Test } from "../../interfaces/Test";
import { TestListState } from "../../reducers/TestListReducer";
import { ApplicationState } from '../../store';
import CircularProgress from '@material-ui/core/CircularProgress';
import FormatListBulletedIcon from '@material-ui/icons/FormatListBulleted';
import { Redirect } from 'react-router';
import { useHistory, Link } from 'react-router-dom';


type StateProps = {
    testListState: TestListState | undefined;
}
type ComponentProps = {
    keyword: string;
}
type DispatchProps = typeof testListActionCreators

type TestSearchResultProps = StateProps & DispatchProps & ComponentProps;


class TestSearchResult extends React.Component<TestSearchResultProps> {
    constructor(props: TestSearchResultProps) {
        super(props);

        this.props.searchTests(this.props.keyword);
    }

    componentDidUpdate(prevProps: TestSearchResultProps, prevState: any, snapshot: any) {
        if (prevProps.keyword !== this.props.keyword) {
            this.props.searchTests(this.props.keyword);
        }
    }

    shouldComponentUpdate(nextProps: TestSearchResultProps, nextState: any) {

        if (nextProps.keyword !== this.props.keyword) {
            console.log('shouldComponentUpdate different keyword - update');
            return true;
        }

        var newTestListState = nextProps.testListState;
        if (newTestListState === undefined) {
            console.log('shouldComponentUpdate newTestList undefined');
            return false;
        }

        if (newTestListState.isLoading !== this.props.testListState?.isLoading) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }

        var newTestList = newTestListState.searchResultTests;
        var oldTestList = this.props.testListState?.searchResultTests;

        if (newTestList === undefined) {
            console.log('shouldComponentUpdate newTestList undefined');
            return false;
        }

        if (newTestList.length !== oldTestList?.length) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }

        newTestList.forEach(element => {
            if (!oldTestList?.includes(element)) {
                console.log('shouldComponentUpdate new element');
                return true;
            }
        });

        return false;
    }

    private renderTestList() {
        if (this.props.testListState === undefined || this.props.testListState.searchResultTests === undefined || this.props.testListState?.isLoading) {
            return (
                <div>
                    Loading...<br />
                    <CircularProgress />
                </div>

            );
        }

        return this.props.testListState.searchResultTests.map((test: Test) => {
            return (
                <Link to={'/testAttempt/' + test.Id}><li className="list-group-item" >
                    <img src={'https://picsum.photos/id/' + test.Id + '/50/50/'} alt={test.Id.toString()} className="rounded-circle" />
                    <span>{test.Title}</span>
                </li></Link>
            );
        });
    }

    render() {
        return (
            <React.Fragment>
                <div className="card {{class}}">
                    <div className="card-header">
                        <div className="form-inline">
                            <div className="form-group">
                                <FormatListBulletedIcon color="primary" />
                            </div>
                        <div className="form-group">
                                <h4>Search results for keyword: {this.props.keyword}</h4>
                            </div>
                        </div>
                    </div>
                    <div className="card-body">
                        {this.props.testListState?.searchResultTests?.length === 0 ? <h5>No results</h5> :
                        <ul className="list-group">
                            {this.renderTestList()}
                         </ul>}
                    </div>
                </div>
            </React.Fragment>
            );
    }
}


const mapStateToProps = (state: ApplicationState, ownProps: any) => ({
    keyword: ownProps.match.params.keyword,
    testListState: state.testList
});

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
            searchTests: testListActionCreators.searchTests,
        },
        dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(TestSearchResult);