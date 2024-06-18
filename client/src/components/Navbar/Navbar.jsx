import  './Navbar.scss'
import React, {useState} from 'react';
import { Link } from 'react-router-dom';

export default function Navbar() {
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
            </div>
            <ul className={`navbar__links ${isActive ? 'active' : ''}`}>
                <li><Link to="/" onClick={() => setIsActive(false)}>Home</Link></li>
                <li><Link to="/tables" onClick={() => setIsActive(false)}>Tables</Link></li>
                <li><Link to="/tables/new" onClick={() => setIsActive(false)}>New Table</Link></li>
            </ul>
        </nav>
    )
        ;
}
