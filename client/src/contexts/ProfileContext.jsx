import React, {createContext, useContext, useState} from "react";

const ProfileContext = createContext(null);

export const useProfile = () => {
    return useContext(ProfileContext);
};

export const ProfileContextProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [profile, setProfile] = useState(null)
    
    const login = () => setIsAuthenticated(true);
    const logout = () => setIsAuthenticated(false);

    return (
        <ProfileContext.Provider value={{profile, setProfile, isAuthenticated, login, logout }}>
            { children }
        </ProfileContext.Provider>
    );
};