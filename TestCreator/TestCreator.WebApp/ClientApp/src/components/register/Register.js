var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};
/* eslint-disable import/first */
import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import { Button, TextField, Modal } from '@material-ui/core';
import { Field, reduxForm } from 'redux-form';
import { userActionCreators } from "../../actions/UserActions";
import { Link, Redirect } from "react-router-dom";
import './Register.css';
import { makeStyles, createStyles } from '@material-ui/core/styles';
const validate = (values) => {
    var _a, _b;
    console.log('validate');
    console.log(values);
    const errors = {};
    const requiredFields = ['Username', 'Email', 'Password', 'ConfirmPassword'];
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Field is required';
        }
    });
    if (values.Email &&
        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.Email)) {
        errors.Email = 'Invalid email address';
    }
    ;
    if (((_a = values.Password) === null || _a === void 0 ? void 0 : _a.length) <= 8) {
        errors.Password = 'Password is too short (must be longer than 8 characters).';
    }
    ;
    if (((_b = values.ConfirmPassword) === null || _b === void 0 ? void 0 : _b.length) <= 8) {
        errors.ConfirmPassword = 'Password is too short (must be longer than 8 characters).';
    }
    ;
    if (values.Password !== values.ConfirmPassword) {
        errors.Password = 'Password and confirm password field value must be same';
        errors.ConfirmPassword = 'Password and confirm password field value must be same';
    }
    ;
    console.log(errors);
    return errors;
};
const renderTextField = (_a) => {
    var { input, label, type, meta: { touched, error } } = _a, custom = __rest(_a, ["input", "label", "type", "meta"]);
    return (React.createElement("div", null,
        React.createElement(TextField, Object.assign({ fullWidth: true, label: label, hintText: label, floatingLabelText: label, type: type, error: touched && error, helperText: error }, custom, input))));
};
const useStyles = makeStyles((theme) => createStyles({
    paper: {
        position: 'absolute',
        width: 400,
        backgroundColor: theme.palette.background.paper,
        border: '2px solid #000',
        boxShadow: theme.shadows[5],
        padding: theme.spacing(2, 4, 3),
    },
}));
function getModalStyle() {
    const top = 50;
    const left = 50;
    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`,
    };
}
const RedirectModal = () => {
    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);
    return (React.createElement(Modal, { open: true, "aria-labelledby": "simple-modal-title", "aria-describedby": "simple-modal-description" },
        React.createElement("div", { style: modalStyle, className: classes.paper }, "Register successfully, redirecting to login page...")));
};
class Register extends React.Component {
    constructor(props) {
        super(props);
        this.onSubmit = this.onSubmit.bind(this);
        this.state = {
            redirect: false
        };
    }
    onSubmit(props) {
        console.log('onSubmit');
        this.props.addUser(props);
    }
    componentDidUpdate(prevProps, prevState, snapshot) {
        var _a, _b;
        console.log('componentDidUpdate');
        if (((_a = prevProps.user) === null || _a === void 0 ? void 0 : _a.isCreated) === false && ((_b = this.props.user) === null || _b === void 0 ? void 0 : _b.isCreated) === true) {
            console.log('will redirect');
            setTimeout(() => {
                this.setState({
                    redirect: true
                });
            }, 2500);
        }
    }
    render() {
        var _a, _b, _c;
        const { handleSubmit, pristine, reset, submitting } = this.props;
        if (this.state.redirect) {
            return React.createElement(Redirect, { to: "/login" });
        }
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: "register-container" },
                React.createElement("img", { id: "register-img", className: "register-img", src: "//ssl.gstatic.com/accounts/ui/avatar_2x.png" }),
                React.createElement("h2", { className: "register-title" }, "Register"),
                React.createElement("form", { className: "register-form", autoComplete: "on", onSubmit: handleSubmit(this.onSubmit) },
                    ((_a = this.props.user) === null || _a === void 0 ? void 0 : _a.isFaulted) ?
                        React.createElement("div", { className: "error-card" }, "Errors furing registering user. Please try again or contact us.") : null,
                    ((_b = this.props.user) === null || _b === void 0 ? void 0 : _b.isCreated) ? React.createElement(RedirectModal, null) : null,
                    ((_c = this.props.user) === null || _c === void 0 ? void 0 : _c.isCreated) ?
                        React.createElement("div", { className: "error-card" }, "Errors furing registering user. Please try again or contact us.") : null,
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Field, { required: true, type: "text", name: "Username", component: renderTextField, label: "Username" })),
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Field, { required: true, type: "text", name: "Email", component: renderTextField, label: "E-mail" })),
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Field, { required: true, type: "password", name: "Password", component: renderTextField, label: "Password" })),
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Field, { required: true, type: "password", name: "ConfirmPassword", component: renderTextField, label: "Confirm password" })),
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Field, { type: "text", name: "DisplayName", component: renderTextField, label: "Display name" })),
                    React.createElement("div", { className: "form-group" },
                        React.createElement(Button, { variant: "contained", color: "primary", type: "submit", disabled: pristine || submitting }, "Register"),
                        React.createElement(Button, { variant: "contained", disabled: pristine || submitting, onClick: reset }, "Clear Values"),
                        React.createElement(Button, { variant: "contained", component: Link, to: "/" }, "Go Back"))))));
    }
}
const mapStateToProps = (state) => ({
    user: state.user
});
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        addUser: userActionCreators.addUser,
    }, dispatch);
};
export default connect(mapStateToProps, mapDispatchToProps)(reduxForm({
    form: 'registerForm',
    validate
})(Register));
//# sourceMappingURL=Register.js.map