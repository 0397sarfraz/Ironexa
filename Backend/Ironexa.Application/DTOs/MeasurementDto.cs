using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class MeasurementDto
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public string? OtherDetails { get; set; }
    }
}
