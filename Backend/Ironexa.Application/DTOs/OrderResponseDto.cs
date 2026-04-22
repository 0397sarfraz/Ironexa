using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string Phone { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Advance { get; set; }
        public decimal Remaining { get; set; }

        public string Status { get; set; }

        public string OrderDate { get; set; }
    }
}
