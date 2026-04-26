using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Course { get; set; }
    }
}