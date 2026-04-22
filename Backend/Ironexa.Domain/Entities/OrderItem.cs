using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Ironexa.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public  string ProductName { get; set; } = null!;
        public string DesignImage { get; set; } = null!;
        public decimal RatePerKg { get; set; }
        public decimal? EstimatedWeight {  get; set; }
        public decimal? FinalWeight { get; set; }
        public decimal? TotalAmount {  get; set; }
        public Measurement Measurement { get; set; }

    }
}
