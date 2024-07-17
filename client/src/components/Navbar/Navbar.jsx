import  './Navbar.scss'
import React, {useState} from 'react';
import { Link } from 'react-router-dom';
import {useProfile} from "../../contexts/ProfileContext.jsx";


export default function Navbar() {
    const {logout, profile} = useProfile();
    const [isActive, setIsActive] = useState(false);
    const handleToggle = () => {
        setIsActive(!isActive);
    };
    
    return (
        <nav className="navbar">
            <div className="navbar__brand">
                <Link to="/">MyApp</Link>
            </div>
            <div className="navbar__toggle" id="navbarToggle" onClick={handleToggle}>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
            </div>
            <ul className={`navbar__links ${isActive ? 'active' : ''}`}>
                <li><Link to="/" onClick={() => setIsActive(false)}>Home</Link></li>
                <li><Link to="/tables" onClick={() => setIsActive(false)}>Tables</Link></li>
                <li><Link to="/user" onClick={() => setIsActive(false)}>Account</Link></li>
                {profile ? (
                    <li>
                        <Link to="/" onClick={() => {
                        setIsActive(false);
                        logout();
                        }}>Sign Out</Link>
                    </li>
                ) : (
                    <li><Link to="/sign-in" onClick={() => setIsActive(false)}>Sign In</Link></li>
                )}
                <li><Link to="/sign-up" onClick={() => setIsActive(false)}>Sign Up</Link></li>
            </ul>
        </nav>
    )
        ;
}
