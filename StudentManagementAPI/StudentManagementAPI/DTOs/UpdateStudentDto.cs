using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Course { get; set; }
    }
}