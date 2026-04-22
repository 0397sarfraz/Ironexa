import React, { useState } from 'react'
import backgroundImage from '../assets/background-image.avif'
import API from '../services/api'
import toast from 'react-hot-toast'
import { useNavigate } from 'react-router-dom'
const Login = () => {

    const navigate=useNavigate()
    const[form, setForm]=useState({
        userName:"",
        password:""
    })

    const handleChange=(e)=>{
        setForm({...form, [e.target.name]:e.target.value})
    }
    const handleSubmit = async (e) => {
        e.preventDefault()

        try{
            const res=await API.post("/account/login", form)
            localStorage.setItem("token", res.data.token);
            console.log(res.data)
            navigate("/orders")
        }
        catch(error){
            console.log(error)
            toast.error("error while login")
        }
        // Handle login logic here
    }
  return (
    <div className='min-h-screen bg-cover bg-center flex justify-center items-center' style={{backgroundImage:` linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(${backgroundImage})`}}>
        <form onSubmit={handleSubmit} className='bg-white/10 backdrop-blur-2xl p-8 rounded-2xl shadow-2xl w-80 text-white'>
        <h2 className="text-xl font-bold mb-4 text-center">Login</h2>
            <input className='w-full p-2 border rounded mb-3' 
            type="text"
            placeholder='Username' 
            name="userName"
            onChange={handleChange} />
            <input className='w-full p-2 border rounded mb-3' 
            type="password" 
            placeholder='Password' 
            name='password'
            onChange={handleChange} />
            <button type='submit' className='w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600'>
                Login
            </button>
        </form>
    </div>
  )
}

export default Login