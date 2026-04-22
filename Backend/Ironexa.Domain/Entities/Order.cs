using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }


    }
}
