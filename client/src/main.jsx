import './index.scss'
import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import {ProfileContextProvider} from "./contexts/ProfileContext.jsx";
import ProtectedRoutes from "./pages/ProtectedRoutes.jsx";

import Layout from "./pages//Layout/Layout.jsx";
import Tables from './pages/Tables/Tables.jsx'
import NewTableForm  from "./pages/AddTable/AddTable.jsx";
import Home from './pages/Home/Home.jsx';
import SignIn from "./pages/SignIn/SignIn.jsx";
import SignUp from "./pages/SignUp/SignUp.jsx";

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <ProfileContextProvider>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<Layout/>}>
                        
                        <Route path='/' element={<Home/>}/>
                       
                        <Route path='/sign-in' element={<SignIn/>}/>
                        <Route path='/sign-up' element={<SignUp/>}/>
                        
                        <Route element={<ProtectedRoutes/>}>
                            <Route path='/tables' element={<Tables/>}/>
                            <Route path='/tables/new' element={<NewTableForm/>}/>
                        </Route>
                        
                    </Route>
                </Routes>
            </BrowserRouter>            
        </ProfileContextProvider>
    </React.StrictMode>
)
