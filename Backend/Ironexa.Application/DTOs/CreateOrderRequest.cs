using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class CreateOrderRequest
    {
        public CustomerDto Customer { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public PaymentDto AdvancePayment {  get; set; }
        public int? OrderId { get; set; }
    }
}
