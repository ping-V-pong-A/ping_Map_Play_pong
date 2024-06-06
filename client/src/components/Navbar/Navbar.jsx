import React from 'react';
import { Link } from 'react-router-dom';

function Navbar() {
    return (
        <div className="navbar">
            <Link to="/tables">
                <button>Tables</button>
            </Link>
        </div>
    );
}

export default Navbar;
