using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
    }
}
