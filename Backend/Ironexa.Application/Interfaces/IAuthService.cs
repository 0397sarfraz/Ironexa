using Ironexa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
