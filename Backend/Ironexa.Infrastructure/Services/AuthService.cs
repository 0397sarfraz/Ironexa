using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Ironexa.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ironexa.Infrastructure.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager, IOptions<AppSettings> _appSettings): IAuthService
    {
        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var getUser= await userManager.FindByNameAsync(request.UserName);
            if (getUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.UserName
                };
               var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return "User registered successfully";
                }
                else
                {
                    return "User registration failed";
                }
            }
            else
            {
                return "User already exists";
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
               return null;
            }
            else
            {
                var getroles = await userManager.GetRolesAsync(user);
                var claims=new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                foreach(var role in getroles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _appSettings.Value.JwtIssuer,
                    audience: _appSettings.Value.JwtAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );
                return new AuthResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
        }
    }
}
