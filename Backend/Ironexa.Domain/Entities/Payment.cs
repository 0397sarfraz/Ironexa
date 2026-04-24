using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; } = null!;
        public string PaymentMode { get; set; } = null!;
        public DateTime PaymentDate { get; set; }= DateTime.UtcNow;
    }
}
