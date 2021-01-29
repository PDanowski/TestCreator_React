import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import WhatshotIcon from '@material-ui/icons/Whatshot';
import AllInclusiveIcon from '@material-ui/icons/AllInclusive';
import SortIcon from '@material-ui/icons/Sort';
import CircularProgress from '@material-ui/core/CircularProgress';
import './TestList.css';
import { testListActionCreators } from "../../actions/TestListActions";
import { Test } from "../../interfaces/Test";
import { TestListState } from "../../reducers/TestListReducer";
import { ApplicationState } from '../../store';


type StateProps = {
    testListState: TestListState | undefined;
}
type ComponentProps = {
    type: string;
}
type DispatchProps = typeof testListActionCreators

type TestListProps = StateProps & DispatchProps & ComponentProps;
type TestListComponentState = { title: string, type: string };

class TestList extends React.Component<TestListProps, TestListComponentState>{

    constructor(props: TestListProps) {
        super(props);

        this.getIcon = this.getIcon.bind(this);
        this.getTitle = this.getTitle.bind(this);
        
        this.state = {
            type: this.props.type,
            title: this.getTitle()
        };

        this.props.getTestList(this.props.type);
    }

    private getIcon() {
        switch (this.props.type) {
            case "latest":
            default:
                return <WhatshotIcon color="primary"/>;
            case "random":
                return <AllInclusiveIcon color="primary"/>;
            case "byTitle":
                return <SortIcon color="primary"/>;
        }
    }

    private getTitle() {
        switch (this.props.type) {
            case "latest":
            default:
                return "Latest tests";
            case "random":
                return "Random tests";
            case "byTitle":
                return "Tests sorted by title";
        } 
    }

    shouldComponentUpdate(nextProps: TestListProps, nextState: TestListComponentState) {

        if (this.state.type != nextState.type) {
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

        var newTestList = newTestListState.testLists[nextProps.type];
        var oldTestList = this.props.testListState?.testLists[nextProps.type];

        if (newTestList === undefined) {
            console.log('shouldComponentUpdate newTestList undefined');
            return false;
        }

        if (newTestList.length !== oldTestList?.length) {
            console.log('shouldComponentUpdate different length - update');
            return true;
        }

        newTestList.forEach(element => {
            if (!oldTestList?.includes(element)){
                console.log('shouldComponentUpdate new element');
                return true;
            }
        });

        return false;
    }

    private renderTestList() {
        if (this.props.testListState === undefined || this.props.testListState.testLists === undefined || this.props.testListState?.isLoading) {
            return (
                <div>
                    Loading...<br/>
                    <CircularProgress />
                </div>
                
            );
        }
        var key = this.state.type;
        var list = this.props.testListState.testLists[key];

        if (list === undefined) {
            return (
                <div>
                    Loading...<br/>
                    <CircularProgress />
                </div>
            );
        }

        return list.map((test: Test) => {
            return (
                <li className="list-group-item">
                    <img src={'https://picsum.photos/id/' + test.Id + '/50/50/'} alt={test.Id.toString()} className="rounded-circle" />
                    <span>{test.Title}</span>
                </li>
            );
        });
    }

    render() {
        var cardClassName = 'card ' + this.state.type;
        return (
            <React.Fragment>
                <div className={cardClassName}>
                    <div className="card-header">
                        <div className="form-inline">
                            <div className="form-group">
                                {this.getIcon()}
                            </div>
                            <div className="form-group">
                                    <h4>{this.state.title}</h4>
                            </div>
                         </div>
                    </div>
                    <div className="card-body">
                        <ul className="list-group">
                            {this.renderTestList()}
                        </ul>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

//function mapDispatchToProps(dispatch: any) {
//    return bindActionCreators({
//        getTestList: getTestList,
//    }, dispatch);
//}

//function mapStateToProps(state: ApplicationState) {
//    return {
//        testList: state.testList
//    };
//}

const mapStateToProps = (state: ApplicationState) => ({
    testListState: state.testList
});

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
            getTestList: testListActionCreators.getTestList,
        },
        dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(TestList);
