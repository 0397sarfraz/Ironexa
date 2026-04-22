using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class AppSettings
    {
        public string JwtKey { get; set; } = string.Empty;
        public string JwtIssuer { get; set; } = string.Empty;
        public string JwtAudience { get; set; } = string.Empty;
    }
}
