using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class RegisterRequest
    {
        public string UserName { get; set; }=null!;
        public string Password { get; set; } = null!;

    }
}
