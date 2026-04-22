using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ironexa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthService _authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                 var result = await _authService.RegisterAsync(request);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
    }
}
