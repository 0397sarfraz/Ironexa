import axios from 'axios'
import React, { useEffect, useState } from 'react'
import API from '../services/api.js'
import toast from 'react-hot-toast'
import { useNavigate, useParams } from 'react-router-dom'

const CreateOrder = () => {

    const { id } = useParams()
    const isEdit = !!id;

    const navigate = useNavigate()
    const [customer, setCustomer] = useState({

        name: "",
        phone: "",
        address: ""
    })

    const [products, setProducts] = useState([
        {
            id: "",
            productName: "",
            ratePerKg: "",
            height: "",
            width: "",
            estimatedWeight: "",
            image: null,
            preview: ""
        }
    ])

    // Add Product
    const addProduct = () => {
        setProducts([...products, {
            productName: "",
            ratePerKg: "",
            height: "",
            width: "",
            estimatedWeight: "",
            image: null,
            preview: ""
        }])
    }

    const [advance, setAdvance] = useState("");

    // Remove Porduct
    const RemoveProduct = (index) => {
        index !== 0 && setProducts(products.filter((_, i) => i !== index))

    }

    //  on adding product
    const handleChange = (index, field, value) => {
        const updated = [...products]
        updated[index][field] = value
        setProducts(updated)
    }

    // handle Image
    const handleImage = (index, file) => {
        const updated = [...products]
        updated[index].image = file
        updated[index].preview = URL.createObjectURL(file)
        setProducts(updated)
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        const formData = new FormData();

        //Customer
        formData.append("customer.name", customer.name);
        formData.append("customer.phone", customer.phone);
        formData.append("customer.address", customer.address);

        // Products
        products.forEach((p, i) => {
            formData.append(`orderItems[${i}].id`, p.id)
            formData.append(`orderItems[${i}].productName`, p.productName);
            formData.append(`orderItems[${i}].ratePerKg`, p.ratePerKg);
            formData.append(`orderItems[${i}].estimatedWeight`, p.estimatedWeight);

            formData.append(`orderItems[${i}].measurement.height`, p.height);
            formData.append(`orderItems[${i}].measurement.width`, p.width);
            if (p.image) {
                formData.append(`orderItems[${i}].images`, p.image);
            }
            else {
                formData.append(`orderItems[${i}].oldImage`, p.oldImage)
            }

        });

        if (isEdit) {
            formData.append("orderId", parseInt(id))
        }

        // Advance
        formData.append("advancePayment.amount", advance);
        formData.append("advancePayment.paymentMode", "Cash");

        try {
            let res;

            if (isEdit) {
                // 🔥 UPDATE API
                res = await API.put("order/update", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                });
            } else {
                // 🔥 CREATE API
                res = await API.post("order/create", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                });
            }
            if (res.data.isSuccess) {
                toast.success(res.data.message);
            }
            else {
                toast.error(res.data.message)
            }

            setTimeout(() => {
                navigate("/orders")
            }, 300)

        } catch (err) {
            console.error(err);
            alert("Error creating order");
        }
    }

    //Total estimated amount
    const totalEstimate = () => {
        return products.reduce((sum, p) => {
            return sum + ((p.estimatedWeight || 0) * (p.ratePerKg || 0));

        }, 0);
    }

    //load existing data

    useEffect(() => {

        if (isEdit) {
            API.get(`/order/${id}`).then(res => {
                const data = res.data;

                setCustomer({
                    name: data.customerName,
                    phone: data.phone,
                    address: data.address
                });

                setProducts(data.items.map(item => ({
                    id: item.id,
                    productName: item.productName,
                    ratePerKg: item.ratePerKg,
                    height: item.height || "",
                    width: item.width || "",
                    estimatedWeight: item.estimatedWeight || "",
                    image: null,
                    oldImage: item.image,
                    preview: item.image
                        ? `https://localhost:7222/Images/${item.image}`
                        : ""
                })));

                setAdvance(data.advance);
            });
        }
        {
            setCustomer({
                    name: "",
                    phone: "",
                    address: ""
                });

                setProducts([{
                    id: 0,
                    productName: "",
                    ratePerKg: "",
                    height: "",
                    width: "",
                    estimatedWeight: "",
                    image: null,
                    oldImage: "",
                    preview: ""                       
                }]);

                setAdvance("");
        }
    }, [id])

    return (
        <>
            <form onSubmit={handleSubmit}>
                <h1 className='text-2xl font-bold p-6'>Create Order</h1>
                <div className='bg-gray-800  p-5 rounded-2xl shadow mb-6'>
                    <h2 className='text-xl mb-3'>Customer Details</h2>
                    <div className='grid grid-cols-3 gap-3'>
                        <input type='text'
                            placeholder='Name'
                            className='p-2 border-2 rounded'
                            value={customer.name}
                            onChange={(e) => { setCustomer({ ...customer, name: e.target.value }) }}
                        />
                        <input type='text'
                            placeholder='Phone'
                            className='p-2 border-2 rounded'
                            value={customer.phone}
                            onChange={(e) => { setCustomer({ ...customer, phone: e.target.value }) }}
                        />
                        <input type='text'
                            placeholder='Address'
                            className='p-2 border-2 rounded'
                            value={customer.address}
                            onChange={(e) => { setCustomer({ ...customer, address: e.target.value }) }}
                        />
                    </div>
                </div>
                {products.map((p, index) => (
                    <div key={index} className='bg-gray-800  p-5 rounded-2xl shadow mb-4'>
                        <div className='flex justify-between items-center mb-3'>
                            <h2>Product</h2>
                            <button
                                type='button'
                                onClick={() => { RemoveProduct(index) }}
                                className='bg-red-500 p-2 rounded cursor-pointer hover:bg-red-700 transition'>Remove</button>
                        </div>
                        <div className='grid grid-cols-2 gap-3'>
                            <input
                                type='text'
                                placeholder='Product Name'
                                className='p-2 border-2 rounded'
                                value={p.productName}
                                onChange={(e) => { handleChange(index, "productName", e.target.value) }}
                            />
                            <input type='text'
                                placeholder='Rate per Kg'
                                className='p-2 border-2 rounded'
                                value={p.ratePerKg}
                                onChange={(e) => { handleChange(index, "ratePerKg", e.target.value) }}
                            />
                            <input type='text'
                                placeholder='Height'
                                className='p-2 border-2 rounded'
                                value={p.height}
                                onChange={(e) => { handleChange(index, "height", e.target.value) }}
                            />
                            <input type='text'
                                placeholder='Width'
                                className='p-2 border-2 rounded'
                                value={p.width}
                                onChange={(e) => { handleChange(index, "width", e.target.value) }}
                            />
                            <input type='text'
                                placeholder='Estimated Weight'
                                className='p-2 border-2 rounded'
                                value={p.estimatedWeight}
                                onChange={(e) => { handleChange(index, "estimatedWeight", e.target.value) }}
                            />
                            <input type='file'
                                className='p-2 border-2 rounded mt-3'
                                onChange={(e) => { handleImage(index, e.target.files[0]) }}
                            />
                            {p.preview &&
                                (<img src={p.preview} alt='preview' className="mt-3 w-32 h-32 object-cover rounded" />)}
                        </div>
                    </div>
                ))}

                <button
                    type='button'
                    onClick={addProduct}
                    className='bg-blue-600 px-4 py-2 rounded mb-6 cursor-pointer hover:bg-blue-800'>+ Add Product</button>

                <div className='bg-gray-800 p-5 rounded-2xl mb-6'>
                    <h2 className='text-lg mb-2'>Summary</h2>
                    <p>Estimated Total: ₹{totalEstimate()}</p>
                    <input type='text' className='mt-3 p-2 w-50 rounded border-2' placeholder='Advance Amount'
                        onChange={(e) => { setAdvance(e.target.value) }}
                    />
                </div>
                <button type='submit'
                    className="bg-green-600 px-6 py-3 rounded text-lg w-full hover:bg-green-800 cursor-pointer"
                >
                    Submit Order 🚀
                </button>
            </form>
        </>

    )
}

export default CreateOrder