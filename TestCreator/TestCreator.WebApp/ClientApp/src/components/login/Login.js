import * as React from 'react';
import { Button, TextField } from '@material-ui/core';
import { login } from '../../services/AuthService';
const Login = (props) => {
    const usernameError = "This field is required. Please enter correct username.";
    const passwordError = "This field is required. Please enter correct password.";
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [showUsernameError, setShowUsernameError] = React.useState(false);
    const [showPasswordError, setShowPasswordError] = React.useState(false);
    function handleSubmit(event) {
        console.log('username: ' + username);
        console.log('password: ' + password);
        login(username, password).then(result => {
            if (result) {
                props.history.push('/');
            }
            setShowUsernameError(true);
            setShowPasswordError(true);
        })
            .catch((err) => {
            console.log(err);
            setShowUsernameError(true);
            setShowPasswordError(true);
        });
        event.preventDefault();
    }
    return (React.createElement(React.Fragment, null,
        React.createElement("div", { className: "login-container" },
            React.createElement("img", { id: "login-img", className: "login-img", src: "//ssl.gstatic.com/accounts/ui/avatar_2x.png" }),
            React.createElement("h2", { className: "login-title" }, "Login"),
            React.createElement("form", { className: "login-form needs-validation" }),
            React.createElement("form", { noValidate: true, autoComplete: "on", onSubmit: handleSubmit },
                React.createElement("div", null,
                    React.createElement(TextField, { required: true, error: showUsernameError, id: "outlined-error-helper-text", label: "Login", helperText: showUsernameError && usernameError, variant: "outlined", value: username, onInput: e => setUsername(e.target.value), placeholder: "Enter username..." })),
                React.createElement("div", null,
                    React.createElement(TextField, { required: true, error: showPasswordError, id: "outlined-error-helper-text", label: "Password", helperText: showPasswordError && passwordError, variant: "outlined", value: password, onInput: e => setPassword(e.target.value), type: "password", placeholder: "Enter password..." })),
                React.createElement("div", { className: "form-check" },
                    React.createElement("label", null,
                        React.createElement("input", { type: "checkbox", name: "remember", value: "remember" }),
                        "Remember me ?")),
                React.createElement(Button, { color: "primary", type: "submit" }, "Login")),
            React.createElement("div", { className: "login-link" },
                React.createElement("a", { href: "#" }, "Password forget")),
            React.createElement("div", { className: "login-link" },
                React.createElement("a", null, "Don't have account ? Register now !")))));
};
export default Login;
//# sourceMappingURL=Login.js.map