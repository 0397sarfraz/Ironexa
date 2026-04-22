using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class OrderItemDto
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }

        public decimal RatePerKg { get; set; }
        public decimal? EstimatedWeight { get; set; }

        public IFormFile? Images { get; set; }
        public string? OldImage { get; set; }

        public MeasurementDto Measurement { get; set; }
    }
}
