using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly StudentDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthManager(StudentDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // 🔹 REGISTER
        public async Task<string> Register(RegisterDto dto)
        {
            // 🔍 Check if user already exists (case-insensitive)
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());

            if (existingUser != null)
            {
                return "User already exists";
            }

            // ✅ Create new user
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password, // ⚠️ Plain text (can upgrade later to hashing)
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User Registered Successfully";
        }

        // 🔹 LOGIN
        public async Task<string?> Login(LoginDto dto)
        {
            // 🔍 Check if user exists (case-insensitive)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());

            if (user == null)
            {
                return "User is not registered";
            }

            // 🔍 Check password
            if (user.Password != dto.Password)
            {
                return "Invalid password";
            }

            // ✅ Generate JWT Token
            return _jwtService.GenerateToken(user.Username, user.Role);
        }
    }
}