using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Advance { get; set; }
        public decimal Remaining { get; set; }

        public List<OrderItemDetailDto> Items { get; set; }
    }
}
