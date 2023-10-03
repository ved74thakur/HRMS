using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;


       




        [Required]
        
        public bool IsActive { get; set; }
       
    }
}

