import React, {createContext, useContext, useState} from "react";

const ProfileContext = createContext(null);

export const useProfile = () => {
    return useContext(ProfileContext);
};

export const ProfileContextProvider = ({ children }) => {
    const [profile, setProfile] = useState(null)

    const login = () => {
        localStorage.setItem('isLoggedIn', true);
        const logoutTime = new Date();
        logoutTime.setMinutes(logoutTime.getMinutes() + 30);
        localStorage.setItem('logoutTime', logoutTime.getTime());

    }
    const logout = () => {
        setProfile(null);
        localStorage.removeItem('isLoggedIn');
        localStorage.removeItem('logoutTime' );
    };

    return (
        <ProfileContext.Provider value={{profile, setProfile, login, logout }}>
            { children }
        </ProfileContext.Provider>
    );
};