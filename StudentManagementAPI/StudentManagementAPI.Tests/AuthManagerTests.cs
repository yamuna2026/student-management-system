using Xunit;
using Moq;
using StudentManagementAPI.Managers;
using StudentManagementAPI.Services;
using StudentManagementAPI.Data;
using StudentManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementAPI.Tests
{
    public class AuthManagerTests
    {
        private StudentDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StudentDbContext(options);
        }

        [Fact]
        public async Task Register_Should_AddUser()
        {
            var context = GetDbContext();
            var jwtMock = new Mock<IJwtService>();

            var manager = new AuthManager(context, jwtMock.Object);

            var dto = new RegisterDto
            {
                Username = "yamuna",
                Password = "123",
                Role = "User"
            };

            var result = await manager.Register(dto);

            Assert.Equal("User Registered Successfully", result);
        }

        [Fact]
        public async Task Login_Should_ReturnNull_WhenInvalidUser()
        {
            var context = GetDbContext();
            var jwtMock = new Mock<IJwtService>();

            var manager = new AuthManager(context, jwtMock.Object);

            var dto = new LoginDto
            {
                Username = "wrong",
                Password = "123"
            };

            var result = await manager.Login(dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_Should_ReturnToken_WhenValidUser()
        {
            var context = GetDbContext();

            var jwtMock = new Mock<IJwtService>();
            jwtMock.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()))
                   .Returns("fake-token");

            var manager = new AuthManager(context, jwtMock.Object);

            await manager.Register(new RegisterDto
            {
                Username = "admin",
                Password = "123",
                Role = "Admin"
            });

            var result = await manager.Login(new LoginDto
            {
                Username = "admin",
                Password = "123"
            });

            Assert.Equal("fake-token", result);
        }
    }
}