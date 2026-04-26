using StudentManagementAPI.DTOs;

namespace StudentManagementAPI.Managers
{
    public interface IAuthManager
    {
        Task<string> Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
    }
}