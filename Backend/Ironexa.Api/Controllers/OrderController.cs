using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ironexa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _orderService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderRequest order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 🔥 exact error milega
            }

            ResponseDto response = new ResponseDto();

            try
            {
                var isAdded = await _orderService.SaveOrderAsyn(order);

                if (isAdded)
                {
                    response.IsSuccess = true;
                    response.Message = "Order created successfully.";
                    response.Status = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    
                    response.Message = "Failed to create Order.";

                    response.Status = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateOrder([FromForm] CreateOrderRequest order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 🔥 exact error milega
            }

            ResponseDto response = new ResponseDto();

            try
            {
                var isAdded = await _orderService.SaveOrderAsyn(order);

                if (isAdded)
                {
                    response.IsSuccess = true;
                    response.Message = "Order updated suceesfully.";

                    response.Status = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;

                    response.Message = "Failed to update Order.";

                    response.Status = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(response);
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orderList = await _orderService.GetAllOrder();
                return Ok(orderList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersByid(int id)
        {
            try
            {
                var orderList = await _orderService.GetOrderById(id);

                return Ok(orderList);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                bool isCancelled = await _orderService.CancelOrder(id);

                if (isCancelled)
                {
                    response.IsSuccess = true;
                    response.Message = "Order cancelled suceesfully.";

                    response.Status = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;

                    response.Message = "Failed to cancel the Order.";

                    response.Status = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(response);
        }
    }
}
