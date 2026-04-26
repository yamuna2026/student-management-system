using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Managers
{
    public interface IStudentManager
    {
        Task<Student> CreateStudent(CreateStudentDto dto);
        Task<Student> UpdateStudent(int id, UpdateStudentDto dto);
        Task<bool> DeleteStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
    }
}