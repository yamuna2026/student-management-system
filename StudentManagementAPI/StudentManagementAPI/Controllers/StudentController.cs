using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Managers;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize] // 🔐 All endpoints require token
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager _manager;

        public StudentController(IStudentManager manager)
        {
            _manager = manager;
        }

        // 🔹 CREATE STUDENT (Admin Only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            var student = await _manager.CreateStudent(dto);

            return Ok(new ApiResponse<object>
            {
                Message = "Student created successfully",
                Data = student
            });
        }

        // 🔹 UPDATE STUDENT (Admin Only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
        {
            var student = await _manager.UpdateStudent(id, dto);

            if (student == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Student updated successfully",
                Data = student
            });
        }

        // 🔹 DELETE STUDENT (Admin Only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manager.DeleteStudent(id);

            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Student deleted successfully",
                Data = null
            });
        }

        // 🔹 GET ALL STUDENTS (Admin Only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _manager.GetAllStudents();

            return Ok(new ApiResponse<object>
            {
                Message = "Students fetched successfully",
                Data = students
            });
        }

        // 🔹 GET STUDENT BY ID (Admin + User)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _manager.GetStudentById(id);

            if (student == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Student fetched successfully",
                Data = student
            });
        }
    }
}