import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import API from "../services/api.js"
import { Eye, SquarePen, Trash2 } from 'lucide-react'


const Orders = () => {

    const [orders, setOrders] = useState([])

    const getOrders = async () => {
        try {
            const res = await API.get("order/getOrders"); // 👈 backend route
            setOrders(res.data);
            console.log(res.data)
        } catch (err) {
            console.error(err);
        }
    };
    useEffect(() => {
        getOrders();
    }, [])

    const handleCancel = async (id) => {

        var res = await API.post(`order/cancel/${id}`);
        if (res.data.status) {
            setOrders(prev => prev.map(o => o.id === id ? { ...o, status: "Cancelled" } : o))
        }

    }

    const navigate = useNavigate()
    return (
        <div className='p-6 bg-gray-900 min-h-screen text-white' >
            <div className='p-2 flex justify-between items-center mb-6'>
                <h2 className='text-2xl font-bold'>Order List</h2>
                <button
                    type='button'
                    onClick={() => {
                        navigate("/createorder")
                    }}
                    className='py-2 px-4 bg-green-600 cursor-pointer rounded hover:bg-green-800'>
                    + Add Order
                </button>
            </div>
            {orders.length === 0 ? (<p className="text-center text-gray-400 mt-10">
                No Orders Found 😔
            </p>) : (
                <table className='w-full bg-gray-800 rounded table-auto'>
                    <thead>
                        <tr className='bg-gray-700'>
                            <th className='p-3'>Order Id</th>
                            <th>Customer Name</th>
                            <th>Phone</th>
                            <th>Total Amount</th>
                            <th>Advance</th>
                            <th>Remaining</th>
                            <th>Status</th>
                            <th>Date</th>
                            <th>Action</th>
                        </tr>

                    </thead>
                    <tbody className='text-center border-t border-gray-700'>

                        {orders.map((order) => (
                            <tr key={order.id}>
                                <td className='p-3'>{order.id}</td>
                                <td>{order.customerName}</td>
                                <td>{order.phone}</td>
                                <td>{order.totalAmount}</td>
                                <td>{order.advance}</td>
                                <td>{order.remaining}</td>
                                <td>
                                    <span
                                        className={`px-2 py-1 rounded ${order.status === "Completed"
                                            ? "bg-green-500"
                                            : order.status === "Cancelled"
                                                ? "bg-red-500"
                                                : "bg-yellow-500"
                                            }`}>{order.status}</span>
                                </td>
                                <td>{order.orderDate}</td>
                                <td className='space-x-2'>
                                    <button
                                        type='button'
                                        onClick={() => { navigate(`/orders/${order.id}`) }}
                                        className='bg-blue-500 px-2 py-1 rounded cursor-pointer hover:bg-blue-700'

                                    ><Eye size={18} /></button>

                                    <button
                                        type='button'
                                        disabled={order.status==="Cancelled"}
                                        onClick={() => { navigate(`/orders/edit/${order.id}`) }}
                                        className='bg-yellow-500 px-2 py-1 rounded cursor-pointer hover:bg-yellow-700'
                                    >
                                        <SquarePen size={18} />
                                    </button>

                                    <button
                                        type='button'
                                        onClick={() => { handleCancel(order.id) }}
                                        className='bg-red-600 px-2 py-1 rounded cursor-pointer hover:bg-red-800'
                                    >
                                        <Trash2 size={18} />
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    )
}

export default Orders