import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Home from './Components/Home.jsx';

function App() {
    const [count, setCount] = useState(0)
    const [apiData, setApiData] = useState(null)

    function handleFetch(){
        fetch('/api/users/users').then(
            res => res.json()
        ).then(
            data => console.log(data)
        )
    }

    return (
        <>
            <Home/>
        </>
    )
}

export default App