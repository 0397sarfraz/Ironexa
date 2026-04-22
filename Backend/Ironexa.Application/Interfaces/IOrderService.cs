using Ironexa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> SaveOrderAsyn(CreateOrderRequest model);
        Task<List<OrderResponseDto>> GetAllOrder();
        Task<OrderDetailDto> GetOrderById(int Id);
        Task<bool> CancelOrder(int id);
    }
}
