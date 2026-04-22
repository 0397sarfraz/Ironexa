import { Outlet, useNavigate } from "react-router-dom";
import Ironexa from '../assets/Ironexa.png'

const Layout=()=>{
    const navigate=useNavigate()

    const handleSignOut=()=>{
        localStorage.removeItem("token");

  navigate("/"); 
    }
    return(
       <div className='flex'>

         {/* Sidebar */}
        <div className='w-60 bg-gray-800 min-h-screen p-4 fixed text-white'>
            <h2 className='text-2xl font-bold mb-6 flex justify-center'>Ironexa</h2>
            <img src={Ironexa} className='w-20 rounded-full ml-15 mb-6'></img>
            <ul className='space-y-3'>
                <li onClick={()=>{navigate('/dashboard')}} className='hover:bg-gray-700 rounded cursor-pointer p-2 hover:scale-105 flex  justify-center'>Dashboard</li>
                <li  onClick={()=>{navigate('/orders')}} className='hover:bg-gray-700 rounded cursor-pointer p-2 hover:scale-105 flex  justify-center'>Orders</li>
                <li  onClick={()=>{navigate('/createorder')}} className='hover:bg-gray-700 rounded cursor-pointer p-2 hover:scale-105 flex  justify-center'>Create Order</li>
                <li  onClick={()=>{handleSignOut()}} className='hover:bg-gray-700 rounded cursor-pointer p-2 hover:scale-105 flex  justify-center'>Sign Out</li>
            </ul>

        </div>
        <div className='flex-1 ml-60  bg-gray-900 min-h-screen text-white p-6'>
            <Outlet/>

        </div>

    </div>
    )
}

export default Layout