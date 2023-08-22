using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Models;
using ATM_BS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IAdminService adminService;

        public AuthController(IConfiguration configuration, IUserService userService, IAdminService adminService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] AuthRequest request)
        {
            AuthResponse? authResponse = null;
            Admin? user = userService.Validate(request.Email, request.Password);

            if (user != null)
            {
                string jwtToken = GetToken(user);
                authResponse = new AuthResponse()
                {
                    Email = user.Email,
                    Token = jwtToken

                };


            }

            return StatusCode(200, authResponse);
        }

        [HttpPost, Route("AddAdmin")]
        public IActionResult Add(AdminDTO adminDTO)
        {
            Console.WriteLine(adminDTO);
            try
            {
                Admin admin = new Admin()
                {
                    Id = adminDTO.Id,
                    Name = adminDTO.Name,
                    Email = adminDTO.Email,
                    Password = adminDTO.Password

                };
                adminService.AddAdmin(admin);
                return StatusCode(200, adminDTO);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetToken(Admin? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Email,user.Email)
                    });

            var expires = DateTime.UtcNow.AddMinutes(10); //expire time

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
