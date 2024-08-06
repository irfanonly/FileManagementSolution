using FileManagement.WebAPI.Models;
using FileManagement.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileManagement.WebAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtService JwtService;

        public AuthController(JwtService jwtService)
        {
            JwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            // In a real application, validate the user from a database
            if (loginRequest.Username == "testuser" && loginRequest.Password == "testpass")
            {
                var token = JwtService.GenerateToken(loginRequest);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
