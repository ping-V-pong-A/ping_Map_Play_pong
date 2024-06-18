import './index.scss'
import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Layout from "./pages/Layout.jsx";
import Tables from './pages/Tables/Tables.jsx'
import NewTableForm  from "./pages/AddTable/AddTable.jsx";
import Home from './pages/Home/Home.jsx';

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <BrowserRouter>
            <Layout>
                <Routes>
                    <Route path='/' element={<Home/>}/>
                    <Route path='/tables' element={<Tables/>}/>
                    <Route path='/tables/new' element={<NewTableForm/>}/>
                </Routes>
            </Layout>
        </BrowserRouter>
    </React.StrictMode>
)
