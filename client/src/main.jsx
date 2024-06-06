import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

//import './index.scss'
import Tables from './pages/Tables/Tables.jsx'
import NewTableForm  from "./pages/NewTable/NewTableForm.jsx";
import Home from './pages/Home/Home.jsx';



ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>

        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Home/>}/>
                <Route path='/tables' element={<Tables/>}/>
                <Route path='/tables/new' element={<NewTableForm/>}/>
            </Routes>
        </BrowserRouter>
    </React.StrictMode>

)
