using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class ResponseDto
    {
        public string Message { get; set; } 
        public bool IsSuccess { get; set; }
        public int Status { get; set; } 
    }
}
