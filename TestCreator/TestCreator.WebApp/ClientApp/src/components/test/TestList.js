import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import WhatshotIcon from '@material-ui/icons/Whatshot';
import AllInclusiveIcon from '@material-ui/icons/AllInclusive';
import SortIcon from '@material-ui/icons/Sort';
import './TestList.css';
import { testListActionCreators } from "../../actions/TestListActions";
class TestList extends React.Component {
    constructor(props) {
        super(props);
        this.getIcon = this.getIcon.bind(this);
        this.getTitle = this.getTitle.bind(this);
        this.state = {
            type: this.props.type,
            title: this.getTitle()
        };
        this.props.getTestList(this.props.type);
        console.log('ctor');
        console.log(this.props.testList);
    }
    getIcon() {
        switch (this.props.type) {
            case "latest":
            default:
                return React.createElement(WhatshotIcon, { color: "primary" });
            case "random":
                return React.createElement(AllInclusiveIcon, { color: "primary" });
            case "byTitle":
                return React.createElement(SortIcon, { color: "primary" });
        }
    }
    getTitle() {
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
    shouldComponentUpdate(nextProps, nextState) {
        var _a, _b;
        console.log('shouldComponentUpdate start');
        if (this.state.type != nextState.type) {
            return true;
        }
        var newTestList = (_a = nextProps.testList) === null || _a === void 0 ? void 0 : _a.testList[nextProps.type];
        var oldTestList = (_b = this.props.testList) === null || _b === void 0 ? void 0 : _b.testList[nextProps.type];
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
        console.log('shouldComponentUpdate end');
        return false;
    }
    renderTestList() {
        console.log('renderTestList start');
        if (this.props.testList === undefined || this.props.testList.testList === undefined) {
            return (React.createElement("div", null, "Loading..."));
        }
        var key = this.state.type;
        var list = this.props.testList.testList[key];
        console.log('this.props.testList.testList.key is: ');
        console.log(list);
        if (list === undefined) {
            return (React.createElement("div", null, "Loading..."));
        }
        return list.map((test) => {
            return (React.createElement("li", { className: "list-group-item" },
                React.createElement("img", { src: 'https://picsum.photos/id/' + test.Id + '/50/50/', alt: test.Id.toString(), className: "rounded-circle" }),
                React.createElement("span", null, test.Title)));
        });
    }
    render() {
        var cardClassName = 'card ' + this.state.type;
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: cardClassName },
                React.createElement("div", { className: "card-header" },
                    React.createElement("div", { className: "form-inline" },
                        React.createElement("div", { className: "form-group" }, this.getIcon()),
                        React.createElement("div", { className: "form-group" },
                            React.createElement("h4", null, this.state.title)))),
                React.createElement("div", { className: "card-body" },
                    React.createElement("ul", { className: "list-group" }, this.renderTestList())))));
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
const mapStateToProps = (state) => ({
    testList: state.testList
});
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        getTestList: testListActionCreators.getTestList,
    }, dispatch);
};
export default connect(mapStateToProps, mapDispatchToProps)(TestList);
//# sourceMappingURL=TestList.js.map