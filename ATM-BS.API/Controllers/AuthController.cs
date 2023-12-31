﻿using ATM_BS.API.DTOS;
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
        //private readonly IUserService userService;
        private readonly IAdminService adminService;

        class AuthException : Exception
        {
            public AuthException() { }
            public AuthException(string message) : base(message) { }
            //public override string Message
            //{
            //    get { return "Auth Failed"; }
            //}
            public string RegErrMessage
            {
                get { return "Registration Failed"; }
            }
            public string ToggleErrMessage
            {
                get { return "Failed to Toggle Admin Status"; }
            }
        }

        public AuthController(IConfiguration configuration, IAdminService adminService)
        {
            this.configuration = configuration;
            //this.userService = userService;
            this.adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] AuthRequest request)
        {
            try
            {
                AuthResponse? authResponse = null;
                Admin? user = adminService.Validate(request.Email, request.Password);

                if (user != null)
                {
                    string jwtToken = GetToken(user);
                    authResponse = new AuthResponse()
                    {
                        Email = user.Email,
                        Token = jwtToken

                    };


                }
                else
                {
                    throw new AuthException("Auth failed");
                }

                return StatusCode(200, authResponse);
            }
            catch (AuthException ex)
            {
                //throw new Exception(ex.Message);
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPost, Route("AddAdmin")]
        public IActionResult Add(AdminDTO adminDTO)
        {
            //Console.WriteLine(adminDTO);
            try
            {
                Admin check1 = adminService.CheckEmail(adminDTO.Email);
                if(check1 != null)
                {
                    throw new AuthException("Admin with email already exists");
                }
                Admin check2 = adminService.CheckId(adminDTO.Id);
                if(check2 != null)
                {
                    throw new AuthException("Admin with Id already exists");
                }

                Admin admin = new Admin()
                {
                    Id = adminDTO.Id,
                    Name = adminDTO.Name,
                    Email = adminDTO.Email,
                    Password = adminDTO.Password,
                    Enable = true

                };
                adminService.AddAdmin(admin);
                return StatusCode(200, adminDTO);

            }
            catch (AuthException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet,Route("GetAdmins"),Authorize]
        public IActionResult GetAdmins()
        {
            try
            {
                var admins = adminService.GetAdmins();
                return StatusCode(200, admins);
            }
            catch(Exception) { throw; }
        }

        [HttpPut,Route("ToggleAdmin/{adminId}"), Authorize]
        public IActionResult ToggleAdmin(int adminId)
        {
            try
            {
                Admin admin = adminService.GetAdmin(adminId);
                admin.Enable = !admin.Enable;
                adminService.EditAdmin(admin);
                return StatusCode(200, admin);
            }
            catch(AuthException ex)
            {
                return StatusCode(400, ex.ToggleErrMessage);
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
