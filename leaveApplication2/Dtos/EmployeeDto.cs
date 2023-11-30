using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    public class EmployeeDto
    {
        public long employeeId { get; set; }
        public string employeeCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string firstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string lastName { get; set; } = string.Empty;



        [Required]
        [MaxLength(255)]
        public string employeeEmail { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;



        [Required]

        public bool IsActive { get; set; }

    }
   
}

    
