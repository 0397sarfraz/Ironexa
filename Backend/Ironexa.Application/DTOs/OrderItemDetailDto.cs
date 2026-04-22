using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class OrderItemDetailDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string Image { get; set; }

        public decimal RatePerKg { get; set; }

        public decimal? EstimatedWeight { get; set; }
        public decimal? FinalWeight { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
    }
}

