import * as React from 'react';
import { Button, TextField } from '@material-ui/core';
import { login } from '../../services/AuthService';


const Login = (props: any) => {

    const usernameError = "This field is required. Please enter correct username.";
    const passwordError = "This field is required. Please enter correct password.";

    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');

    const [showUsernameError, setShowUsernameError] = React.useState(false);
    const [showPasswordError, setShowPasswordError] = React.useState(false);

    function handleSubmit(event: any) {
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

    return (
            <React.Fragment>
                <div className="login-container">
                    <img id="login-img" className="login-img" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"/>
                    <h2 className="login-title">Login</h2>
                    <form className="login-form needs-validation">

                    </form>

                    <form noValidate autoComplete="on" onSubmit={handleSubmit}>
                        <div>
                            <TextField
                            required 
                            error={showUsernameError}
                            id="outlined-error-helper-text"
                            label="Login"
                            helperText={showUsernameError && usernameError}
                            variant="outlined"
                            value={username}
                            onInput={e => setUsername(e.target.value)}
                            placeholder="Enter username..."
                            />
                        </div>
                        <div>
                            <TextField
                            required 
                            error={showPasswordError}
                            id="outlined-error-helper-text"
                            label="Password"
                            helperText={showPasswordError && passwordError}
                            variant="outlined"
                            value={password}
                            onInput={e => setPassword(e.target.value)}
                            type="password"
                            placeholder="Enter password..."
                            />
                        </div>
                        <div className="form-check">
                            <label>
                                <input type="checkbox" name="remember" value="remember" />
                                Remember me ?
                            </label>
                        </div>
                        <Button color="primary" type="submit">Login</Button>
                    </form>


                    <div className="login-link">
                        <a href="#">
                            Password forget
                        </a>
                    </div>
                    <div className="login-link">
                        <a>
                            Don't have account ? Register now !
                        </a>
                    </div>
                </div>
            </React.Fragment>
     );
}

export default Login;