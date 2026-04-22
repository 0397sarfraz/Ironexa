import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import API from '../services/api.js'

const OrderDetail = () => {

  const { id } = useParams()
  const [order, setOrder] = useState(null)

  useEffect(() => {
    API.get(`/order/${id}`)
      .then(res => setOrder(res.data))
      .catch(err => console.log(err))
  }, [id])

  if (!order)
    return (
      <div className='flex justify-center items-center h-screen text-white'>
        Loading...
      </div>
    )

  return (
    <div className='p-6 bg-gray-950 min-h-screen text-white'>

      {/* Header */}
      <div className='flex justify-between items-center mb-6'>
        <h1 className='text-3xl font-bold'>Order #{order.id}</h1>

        <span className={`px-4 py-1 rounded-full text-sm ${
          order.status === "Completed"
            ? "bg-green-500"
            :order.status==="Cancelled"?"bg-red-500"
            : "bg-yellow-500"
        }`}>
          {order.status}
        </span>
      </div>

      {/* Customer Info */}
      <div className='bg-gray-800 p-6 rounded-2xl shadow-lg mb-6'>
        <h2 className='text-xl font-semibold mb-3'>Customer Info</h2>

        <div className='grid grid-cols-3 gap-4 text-gray-300'>
          <p><b>Name:</b> {order.customerName}</p>
          <p><b>Phone:</b> {order.phone}</p>
          <p><b>Address:</b> {order.address}</p>
        </div>
      </div>

      {/* Products */}
      <div className='grid grid-cols-3 gap-6'>
        {order.items.map((item) => (
          <div
            key={item.id}
            className='bg-gray-800 p-4 rounded-2xl shadow-lg hover:scale-105 transition'
          >

            <img
              src={
                item.image
                  ? `https://localhost:7222/Images/${item.image}`
                  : "/no-image.png"
              }
              alt='product'
              className='w-full h-44 object-cover rounded-xl mb-3'
            />

            <h3 className='text-lg font-semibold'>{item.productName}</h3>

            <div className='text-sm text-gray-300 mt-2 space-y-1'>
              <p>Rate: ₹{item.ratePerKg}/kg</p>
              <p>Est Weight: {item.estimatedWeight || "-"}</p>
              <p>Final Weight: {item.finalWeight || "-"}</p>
            </div>

            <div className='mt-3 border-t border-gray-600 pt-2'>
              <p className='text-green-400 font-semibold'>
                ₹{item.totalAmount || "Pending"}
              </p>
            </div>

            <p className='text-xs text-gray-400 mt-2'>
              Size: {item.height && item.width
                ? `${item.height} x ${item.width}`
                : "N/A"}
            </p>

          </div>
        ))}
      </div>

      {/* Payment Summary */}
      <div className='bg-gray-800 p-6 rounded-2xl shadow-lg mt-6'>
        <h2 className='text-xl font-semibold mb-3'>Payment Summary</h2>

        <div className='grid grid-cols-3 text-center gap-4'>

          <div>
            <p className='text-gray-400'>Total</p>
            <p className='text-lg font-bold'>
              {order.totalAmount || "Pending"}
            </p>
          </div>

          <div>
            <p className='text-gray-400'>Advance</p>
            <p className='text-lg font-bold text-yellow-400'>
              ₹{order.advance}
            </p>
          </div>

          <div>
            <p className='text-gray-400'>Remaining</p>
            <p className='text-lg font-bold text-red-400'>
              {order.totalAmount > 0
                ? `₹${order.remaining}`
                : "Pending"}
            </p>
          </div>

        </div>
      </div>

    </div>
  )
}

export default OrderDetail