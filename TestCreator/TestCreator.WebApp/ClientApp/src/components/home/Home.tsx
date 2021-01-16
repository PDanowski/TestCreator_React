import * as React from 'react';
import TestList from '../test/TestList';

class Home extends React.PureComponent<any> {
    public render() {
        return (
            <React.Fragment>
                <div className="row">
                    <div className="col-lg-4">
                        <TestList type="latest"/>
                    </div>
                    <div className="col-lg-4">
                        <TestList type="byTitle"/>
                    </div>
                    <div className="col-lg-4">
                        <TestList type="random"/>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

export default Home;