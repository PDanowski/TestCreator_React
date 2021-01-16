import * as React from 'react';
import TestList from '../test/TestList';
class Home extends React.PureComponent {
    render() {
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: "row" },
                React.createElement("div", { className: "col-lg-4" },
                    React.createElement(TestList, { type: "latest" })),
                React.createElement("div", { className: "col-lg-4" },
                    React.createElement(TestList, { type: "byTitle" })),
                React.createElement("div", { className: "col-lg-4" },
                    React.createElement(TestList, { type: "random" })))));
    }
}
export default Home;
//# sourceMappingURL=Home.js.map