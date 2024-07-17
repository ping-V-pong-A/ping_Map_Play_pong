import './index.scss'
import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import {ProfileContextProvider} from "./contexts/ProfileContext.jsx";
import ProtectedRoutes from "./pages/ProtectedRoutes.jsx";

import Layout from "./pages//Layout/Layout.jsx";
import Tables from './pages/Tables/Tables.jsx'
import Table from './pages/Table/Table.jsx'
import NewTableForm  from "./pages/AddTable/AddTable.jsx";
import Home from './pages/Home/Home.jsx';
import SignIn from "./pages/SignIn/SignIn.jsx";
import SignUp from "./pages/SignUp/SignUp.jsx";
import AdminPage from "./pages/AdminPage/AdminPage.jsx";
import Account from "./pages/Account/Account.jsx";


ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <ProfileContextProvider>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<Layout/>}>
                        
                        <Route path='/' element={<Home/>}/>
                       
                        <Route path='/sign-in' element={<SignIn/>}/>

                        <Route path='/sign-up' element={<SignUp/>}/>
                        <Route path='/admin' element={<AdminPage/>}/>

                            <Route path='/tables' element={<Tables/>}/>
                            <Route path='/tables/table/:id' element={<Table/>}/>
                            <Route path='/tables/new' element={<NewTableForm/>}/>
                            <Route path='/user' element={<Account/>}/>
                        
                        <Route element={<ProtectedRoutes/>}>
                        </Route>
                        
                    </Route>
                </Routes>
            </BrowserRouter>            
        </ProfileContextProvider>
    </React.StrictMode>
)
