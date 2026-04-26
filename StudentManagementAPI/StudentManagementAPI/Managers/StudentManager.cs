using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Managers
{
    public class StudentManager : IStudentManager
    {
        private readonly StudentDbContext _context;

        public StudentManager(StudentDbContext context)
        {
            _context = context;
        }

        // 🔹 Create Student
        public async Task<Student> CreateStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                StudentName = dto.StudentName,
                Course = dto.Course,
                CreatedOn = DateTime.Now
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        // 🔹 Update Student
        public async Task<Student> UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return null;

            student.StudentName = dto.StudentName;
            student.Course = dto.Course;
            student.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();

            return student;
        }

        // 🔹 Delete Student
        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }

        // 🔹 Get All Students
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // 🔹 Get By Id
        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }
    }
}