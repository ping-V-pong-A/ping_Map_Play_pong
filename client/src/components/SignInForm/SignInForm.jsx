import React, {useState} from "react";
import {Link} from "react-router-dom";

export default function SignInForm({onSave, onCancel}) {
    const [user, setUser] = useState({
        email: "",
        password: ""
    });

    const onSubmit = e => {
        e.preventDefault();
        return onSave(user);
    }

    return (
        <form onSubmit={onSubmit}>
            <div>
                <label htmlFor="email-sign-in" id="form1"/>
                <input
                    value={user.email}
                    onChange={e => setUser({...user, email: e.target.value})}
                    placeholder="Email"
                    type="email"
                    name="email"
                    id="email-sign-in"
                />
            </div>
            <div>
                <label htmlFor="password-sign-in" id="form1"/>
                <input
                    value={user.password}
                    onChange={e => setUser({...user, password: e.target.value})}
                    placeholder="Password"
                    type="password"
                    name="password"
                    id="password-sign-in"
                />
            </div>
            <div>
                <Link to="/sign-up">
                    <p>SignUp</p>
                </Link>
            </div>

            <button type="submit">SignIn</button>
            <button type="button" onClick={onCancel}>Cancel</button>

        </form>
    );

}