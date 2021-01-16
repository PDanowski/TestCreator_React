import * as React from 'react';

const About: React.FC = () => {
    const marginStyle = {
        marginTop: "10px",
        padding: "10px"
    };

    return (
        <React.Fragment>
            <h2 style={marginStyle}> About</h2>
            <div>
                TestCreator - is it free and ready to production use system, used ASP.NET Core and React.
            </div>
        </React.Fragment>
    );
};

export default About;