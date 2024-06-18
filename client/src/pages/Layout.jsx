import React from "react";
import Navbar from "../components/Navbar/Navbar.jsx";

export default function Layout({ children }) {
    return (
        <div>
            <Navbar />
            <main>{children}</main>
        </div>
    );
}