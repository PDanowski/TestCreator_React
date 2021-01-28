/* eslint-disable import/first */
import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from "redux";
import { Button, TextField, Modal } from '@material-ui/core';
import { Field, reduxForm, InjectedFormProps } from 'redux-form';
import { UserState } from "../../reducers/UserReducer";
import { userActionCreators } from "../../actions/UserActions";
import { ApplicationState } from '../../store';
import { User } from "../../interfaces/User";
import { Link, Redirect } from "react-router-dom";
import './Register.css';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';


const validate = (values: any) => {
    console.log('validate');
    console.log(values);
    const errors: any = {};
    const requiredFields = ['Username', 'Email', 'Password', 'ConfirmPassword'];
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Field is required';
        }
    });
    if (values.Email &&
        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.Email)) {
        errors.Email = 'Invalid email address';
    };
    if (values.Password?.length <= 8) {
        errors.Password = 'Password is too short (must be longer than 8 characters).';
    };
    if (values.ConfirmPassword?.length <= 8) {
        errors.ConfirmPassword = 'Password is too short (must be longer than 8 characters).';
    };
    if (values.Password !== values.ConfirmPassword) {
        errors.Password = 'Password and confirm password field value must be same';
        errors.ConfirmPassword = 'Password and confirm password field value must be same';
    };

    console.log(errors);
    return errors;
};

const renderTextField = ({ input, label, type, meta: { touched, error }, ...custom }: any) => (
    <div>
        <TextField
            fullWidth={true}
            label={label}
            hintText={label}
            floatingLabelText={label}
            type={type}
            error={touched && error}
            helperText={error}
            {...custom}
            {...input} />
    </div>
);

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        paper: {
            position: 'absolute',
            width: 400,
            backgroundColor: theme.palette.background.paper,
            border: '2px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3),
        },
    }),
);
function getModalStyle() {
    const top = 50;
    const left = 50;

    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`,
    };
}


type StateProps = {
    user: UserState | undefined;
}
type DispatchProps = typeof userActionCreators
type ExternalProps = StateProps & DispatchProps;
type RegisterProps = InjectedFormProps<User, ExternalProps> & ExternalProps;


const RedirectModal = () => {
    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);

    return (
        <Modal
            open={true}
            aria-labelledby="simple-modal-title"
            aria-describedby="simple-modal-description">
            <div style={modalStyle} className={classes.paper}>
                Register successfully, redirecting to login page...
            </div>
        </Modal>
    );

}


class Register extends React.Component<RegisterProps> {

    constructor(props: RegisterProps) {
        super(props);

        this.onSubmit = this.onSubmit.bind(this);

        this.state = {
            redirect: false
        }
    }

    onSubmit(props: User) {
        console.log('onSubmit');
        this.props.addUser(props);
    }

    componentDidUpdate(prevProps: RegisterProps, prevState: any, snapshot: any) {
        console.log('componentDidUpdate');
        if (prevProps.user?.isCreated === false && this.props.user?.isCreated === true) {
            console.log('will redirect');
            setTimeout(() => {
                this.setState({
                    redirect: true
                });
            }, 2500);
        }
    }

    render() {
        const { handleSubmit, pristine, reset, submitting } = this.props;

        if (this.state.redirect) {
            return <Redirect to="/login"/>;
        }

        return (
            <React.Fragment>
                <div className="register-container">
                    <img id="register-img" className="register-img" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png" />
                    <h2 className="register-title">Register</h2>

                    <form className="register-form" autoComplete="on" onSubmit={handleSubmit(this.onSubmit)}>
                        {this.props.user?.isFaulted ?
                            <div className="error-card">
                                Errors furing registering user. Please try again or contact us.
                        </div> : null}
                        {this.props.user?.isCreated ? <RedirectModal /> : null}
                        {this.props.user?.isCreated ?
                            <div className="error-card">
                                Errors furing registering user. Please try again or contact us.
                        </div> : null}
                        <div className="form-group">
                            <Field required type="text" name="Username" component={renderTextField} label="Username" />
                        </div>
                        <div className="form-group">
                            <Field required type="text" name="Email" component={renderTextField} label="E-mail" />
                        </div>
                        <div className="form-group">
                            <Field required type="password" name="Password" component={renderTextField} label="Password" />
                        </div>
                        <div className="form-group">
                            <Field required type="password" name="ConfirmPassword" component={renderTextField} label="Confirm password" />
                        </div>
                        <div className="form-group">
                            <Field type="text" name="DisplayName" component={renderTextField} label="Display name" />
                        </div>
                        <div className="form-group">
                            <Button variant="contained" color="primary" type="submit" disabled={pristine || submitting}>Register</Button>
                            <Button variant="contained" disabled={pristine || submitting} onClick={reset}>Clear Values</Button>
                            <Button variant="contained" component={Link} to="/">Go Back</Button>
                        </div>
                    </form>
                </div>
            </React.Fragment>
        );

    }
}

const mapStateToProps = (state: ApplicationState) => ({
    user: state.user
});

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        addUser: userActionCreators.addUser,
    },
        dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(reduxForm<User, ExternalProps>({
    form: 'registerForm',
    validate
})(Register));