import React, { useState } from 'react'
import backgroundImage from '../assets/background-image.avif'
import toast from 'react-hot-toast'
import API from '../services/api'
import { useNavigate } from 'react-router-dom'


const Register = () => {
    const navigate=useNavigate();
    const[formData, setFormData] = useState({
        userName: '',
        password: ''
    })

    const handleChange=(e)=>{
        setFormData({...formData, [e.target.name]: e.target.value})
    }

    const  handleSubmit= async(e)=>{
        e.preventDefault()

        try{
            // Handle registration logic here
            const res=await API.post("/account/register", formData)
            console.log(res.data)
            toast.success("Registered Successfully")
            navigate("/")
        } catch (error) {
            console.error("Error registering user:", error)
            toast.error("Error registering user")
        }
    }
    return (
        <div className='min-h-screen flex items-center justify-center bg-cover bg-center' style={{backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(${backgroundImage})` }}>
            <form onSubmit={handleSubmit} className='bg-white/10 backdrop-blur-2xl p-8 rounded-2xl shadow-xl w-80 text-white'>
                <h2 className="text-xl font-bold mb-4 text-center">Register</h2>
                <input type="text"
                    required
                    className='w-full p-2 border rounded mb-3'
                    onChange={handleChange}
                    placeholder="Username" 
                    name="UserName"
                    />
                <input type="password"
                    required
                    className='w-full p-2 border rounded mb-3'
                    onChange={handleChange}
                    placeholder="Password"
                    name="Password"
                     />
                <button type="submit" className='w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600'>
                    Register
                </button>
            </form>
        </div>
    )
}

export default Register