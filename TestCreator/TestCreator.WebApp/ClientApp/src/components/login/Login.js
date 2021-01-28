import * as React from 'react';
import { Button, TextField } from '@material-ui/core';
import { login } from '../../services/AuthService';
import './Login.css';
const Login = (props) => {
    const requiredError = "This field is required. Please enter correct ";
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [showUsernameError, setShowUsernameError] = React.useState(false);
    const [showPasswordError, setShowPasswordError] = React.useState(false);
    const [errors, setErrors] = React.useState(['']);
    function handleSubmit(event) {
        if (username === '' || password === '') {
            setShowUsernameError(true);
            setShowPasswordError(true);
        }
        login(username, password).then(result => {
            if (result) {
                props.history.push('/');
            }
            setErrors(["Invalid username or password"]);
        })
            .catch((err) => {
            console.log(err);
            setErrors(["Error during login operation. Please try again."]);
        });
        event.preventDefault();
    }
    return (React.createElement(React.Fragment, null,
        React.createElement("div", { className: "login-container" },
            React.createElement("img", { id: "login-img", className: "login-img", src: "//ssl.gstatic.com/accounts/ui/avatar_2x.png" }),
            React.createElement("h2", { className: "login-title" }, "Login"),
            React.createElement("form", { className: "login-form", noValidate: true, autoComplete: "on", onSubmit: handleSubmit },
                React.createElement("div", { className: "error-card text-body" }, errors),
                React.createElement("div", { className: "form-group" },
                    React.createElement(TextField, { required: true, fullWidth: true, error: showUsernameError, id: "outlined-error-helper-text", label: "Login", helperText: showUsernameError && (requiredError + 'username'), variant: "outlined", value: username, onInput: e => setUsername(e.target.value), placeholder: "Enter username..." })),
                React.createElement("div", { className: "form-group" },
                    React.createElement(TextField, { required: true, fullWidth: true, error: showPasswordError, id: "outlined-error-helper-text", label: "Password", helperText: showPasswordError && (requiredError + 'password'), variant: "outlined", value: password, onInput: e => setPassword(e.target.value), type: "password", placeholder: "Enter password..." })),
                React.createElement("div", { className: "form-check" },
                    React.createElement("label", null,
                        React.createElement("input", { type: "checkbox", name: "remember", value: "remember" }),
                        "\u00A0Remember me ?")),
                React.createElement(Button, { variant: "contained", color: "primary", type: "submit" }, "Login")),
            React.createElement("div", { className: "login-link" },
                React.createElement("a", { href: "#" }, "Password forget")),
            React.createElement("div", { className: "login-link" },
                React.createElement("a", { href: "\\register" }, "Don't have account ? Register now !")))));
};
export default Login;
//# sourceMappingURL=Login.js.map