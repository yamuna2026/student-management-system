using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(StudentDbContext context)
        {
            // 🔹 Ensure DB created
            context.Database.Migrate();

            // 🔹 Seed Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "admin", Password = "123", Role = "Admin" },
                    new User { Username = "user", Password = "123", Role = "User" }
                );

                context.SaveChanges();
            }

            // 🔹 Create Stored Procedures
            CreateStoredProcedures(context);
        }

        private static void CreateStoredProcedures(StudentDbContext context)
        {
            // 🔹 Create Student
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateStudent')
                EXEC('
                CREATE PROCEDURE sp_CreateStudent
                    @StudentName NVARCHAR(100),
                    @Course NVARCHAR(100)
                AS
                BEGIN
                    INSERT INTO Students (StudentName, Course, CreatedOn)
                    VALUES (@StudentName, @Course, GETDATE())
                END')
            ");

            // 🔹 Get All
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllStudents')
                EXEC('
                CREATE PROCEDURE sp_GetAllStudents
                AS
                BEGIN
                    SELECT * FROM Students
                END')
            ");

            // 🔹 Get By Id
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetStudentById')
                EXEC('
                CREATE PROCEDURE sp_GetStudentById
                    @Id INT
                AS
                BEGIN
                    SELECT * FROM Students WHERE StudentId = @Id
                END')
            ");

            // 🔹 Update
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateStudent')
                EXEC('
                CREATE PROCEDURE sp_UpdateStudent
                    @Id INT,
                    @StudentName NVARCHAR(100),
                    @Course NVARCHAR(100)
                AS
                BEGIN
                    UPDATE Students
                    SET StudentName = @StudentName,
                        Course = @Course,
                        UpdatedOn = GETDATE()
                    WHERE StudentId = @Id
                END')
            ");

            // 🔹 Delete
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteStudent')
                EXEC('
                CREATE PROCEDURE sp_DeleteStudent
                    @Id INT
                AS
                BEGIN
                    DELETE FROM Students WHERE StudentId = @Id
                END')
            ");
        }
    }
}