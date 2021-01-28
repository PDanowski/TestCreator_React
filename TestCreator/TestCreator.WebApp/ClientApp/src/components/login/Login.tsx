import * as React from 'react';
import { Button, TextField } from '@material-ui/core';
import { login } from '../../services/AuthService';
import './Login.css';


const Login = (props: any) => {

    const requiredError = "This field is required. Please enter correct ";

    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');

    const [showUsernameError, setShowUsernameError] = React.useState(false);
    const [showPasswordError, setShowPasswordError] = React.useState(false);

    const [errors, setErrors] = React.useState(['']);

    function handleSubmit(event: any) {
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

    return (
        <React.Fragment>
            <div className="login-container">
                <img id="login-img" className="login-img" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png" />
                <h2 className="login-title">Login</h2>
                <form className="login-form" noValidate autoComplete="on" onSubmit={handleSubmit}>
                    <div className="error-card text-body">
                        {errors}
                    </div>

                    <div className="form-group">
                        <TextField
                            required
                            fullWidth={true}
                            error={showUsernameError}
                            id="outlined-error-helper-text"
                            label="Login"
                            helperText={showUsernameError && (requiredError + 'username')}
                            variant="outlined"
                            value={username}
                            onInput={e => setUsername((e.target as HTMLInputElement).value)}
                            placeholder="Enter username..."/>
                    </div>
                    <div className="form-group">
                        <TextField
                            required
                            fullWidth={true}
                            error={showPasswordError}
                            id="outlined-error-helper-text"
                            label="Password"
                            helperText={showPasswordError && (requiredError + 'password')}
                            variant="outlined"
                            value={password}
                            onInput={e => setPassword((e.target as HTMLInputElement).value)}
                            type="password"
                            placeholder="Enter password..."/>
                    </div>
                    <div className="form-check">
                        <label><input type="checkbox" name="remember" value="remember" />&nbsp;Remember me ?</label>
                    </div>
                    <Button variant="contained" color="primary" type="submit">Login</Button>
                </form>


                <div className="login-link">
                    <a href="#">
                        Password forget
                    </a>
                </div>
                <div className="login-link">
                    <a href="\register">
                        Don't have account ? Register now !
                    </a>
                </div>
            </div>
        </React.Fragment>
    );
}

export default Login;