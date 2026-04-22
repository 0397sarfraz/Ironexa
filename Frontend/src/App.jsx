import React from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Register from './pages/Register'
import Login from './pages/Login';
import { Toaster } from 'react-hot-toast';
import PrivateRoute from './PrivateRoute';
import Dashboard from './pages/Dashboard';
import Orders from './pages/Orders';
import Layout from './Component/Layout'
import CreateOrder from './pages/CreateOrder';
import OrderDetail from './pages/OrderDetail';

const App = () => {
  return (
    <>
      <Toaster position="top-right" />
      <BrowserRouter>
        <Routes>
          <Route path='/register' element={<Register />} />
          <Route path='/' element={<Login />} />
          <Route element={<PrivateRoute><Layout/></PrivateRoute>}>
            <Route path='/orders' element={<Orders/>} />
            <Route path='/dashboard' element={<Dashboard/>}/>
            <Route path='/createorder' element={<CreateOrder/>}/>
            <Route path='/orders/:id' element={<OrderDetail/>}/>
            <Route path="/orders/edit/:id" element={<CreateOrder />} />
          </Route>
         
          
        
        </Routes>
      </BrowserRouter>
    </>

  )
}

export default App