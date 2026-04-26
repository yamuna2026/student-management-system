using Xunit;
using StudentManagementAPI.Managers;
using StudentManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.DTOs;
using System;
using System.Threading.Tasks;

namespace StudentManagementAPI.Tests
{
    public class StudentManagerTests
    {
        private StudentDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StudentDbContext(options);
        }

        [Fact]
        public async Task CreateStudent_Should_AddStudent()
        {
            var context = GetDbContext();
            var manager = new StudentManager(context);

            var dto = new CreateStudentDto
            {
                StudentName = "Yamuna",
                Course = "CSE"
            };

            var result = await manager.CreateStudent(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllStudents_Should_ReturnEmptyList()
        {
            var context = GetDbContext();
            var manager = new StudentManager(context);

            var result = await manager.GetAllStudents();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStudentById_Should_ReturnNull_WhenNotFound()
        {
            var context = GetDbContext();
            var manager = new StudentManager(context);

            var result = await manager.GetStudentById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteStudent_Should_ReturnFalse_WhenNotExists()
        {
            var context = GetDbContext();
            var manager = new StudentManager(context);

            var result = await manager.DeleteStudent(1);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateStudent_Should_ReturnNull_WhenNotFound()
        {
            var context = GetDbContext();
            var manager = new StudentManager(context);

            var dto = new UpdateStudentDto
            {
                StudentName = "Test",
                Course = "IT"
            };

            var result = await manager.UpdateStudent(1, dto);

            Assert.Null(result);
        }
    }
}