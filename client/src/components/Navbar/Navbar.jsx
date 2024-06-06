import React, {useState, useEffect} from 'react';

function Navbar() {



    return (<>
    <div className="navbar">
        <Link to={"/tables"}>
            <button>Tables        
        </button>
    </Link>
</div>
</>
)
    ;
}

export default Navbar;