using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using StudentManagementAPI.Managers; // IMPORTANT

namespace StudentManagementAzureFunctions
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IStudentManager _studentManager; // Inject manager

        // Dependency Injection
        public Function1(ILoggerFactory loggerFactory, IStudentManager studentManager)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _studentManager = studentManager;
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("Timer triggered at: {time}", DateTime.Now);

            // 🔹 CALL GET ALL STUDENTS
            var students = await _studentManager.GetAllStudents();

            // 🔹 LOG RESULT
            _logger.LogInformation("Total Students Count: {count}", students.Count);

            foreach (var student in students)
            {
                _logger.LogInformation("Student: {name}, Course: {course}",
                    student.StudentName,
                    student.Course);
            }

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation("Next run at: {next}", myTimer.ScheduleStatus.Next);
            }
        }
    }
}