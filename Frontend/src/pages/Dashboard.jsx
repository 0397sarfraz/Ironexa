import React, { useEffect, useState } from 'react'
import Ironexa from '../assets/Ironexa.png'
import { Outlet } from 'react-router-dom'
import { useNavigate } from 'react-router-dom'
import API from '../services/api.js'

const Dashboard = () => {
    const navigate = useNavigate()

    const [dashboard, setDashboard] = useState({});

    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await API.get("dashboard/summary");
                setDashboard(res.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchData();
    }, []);
    return (
        <>
            <div className='flex justify-between items-center p-6'>
                <h1 className='text-2xl font-bold'>Dashboard</h1>
                <button onClick={() => { navigate("/createorder") }} className='bg-blue-600 py-2 px-4 rounded hover:bg-blue-700 transition'
                >+ Create Order</button>
            </div>
            <div className='grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8'>
                <div className='bg-gray-800 p-5 rounded-xl shadow hover:scale-105 transition'>
                    <p className='text-gray-400'>Total Orders</p>
                    <h2 className='text-3xl font-bold'>{dashboard.totalOrders || 0}</h2>
                </div>
                <div className="bg-yellow-600 p-5 rounded-xl shadow hover:scale-105 transition">
                    <p>Pending</p>
                    <h2 className='text-3xl font-bold'>{dashboard.pendingOrders|| 0}</h2>
                </div>
                <div className="bg-green-600 p-5 rounded-xl shadow hover:scale-105 transition">
                    <p>Completed</p>
                    <h2 className='text-3xl font-bold'>{dashboard.completedOrders || 0}</h2>
                </div>
                <div className="bg-purple-600 p-5 rounded-xl shadow hover:scale-105 transition">
                    <p>Total Earning</p>
                    <h2 className='text-3xl font-bold'>₹{dashboard.totalEarnings || 0}</h2>
                </div>

            </div>
        </>
    )
}

export default Dashboard