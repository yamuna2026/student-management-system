using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Managers;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // 🔹 REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authManager.Register(dto);

            // ❌ User already exists
            if (result == "User already exists")
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            // ✅ Success
            return Ok(new
            {
                message = result
            });
        }

        // 🔹 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authManager.Login(dto);

            // ❌ User not registered
            if (result == "User is not registered")
            {
                return NotFound(new
                {
                    message = result
                });
            }

            // ❌ Invalid password
            if (result == "Invalid password")
            {
                return Unauthorized(new
                {
                    message = result
                });
            }

            // ✅ Success → return token
            return Ok(new
            {
                token = result
            });
        }
    }
}