using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Domain.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        public int  OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        public decimal Heigth { get; set; }
        public decimal Weigth { get; set; }
        public string? OtherDetails { get; set; }
    }
}
