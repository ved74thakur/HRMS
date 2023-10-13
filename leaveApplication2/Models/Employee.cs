using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace leaveApplication2.Models
{
        [Table("Employees")]
        public class Employee
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long employeeId { get; set; }
            public string employeeCode { get; set; } = string.Empty;

            [Required]
            [MaxLength(255)]
            public string firstName { get; set; } =string.Empty;

            [Required]
            [MaxLength(255)]
            public string lastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "mobileNo is required")]
            [MaxLength(255)]
            public string mobileNo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Gender is required")]
            [ForeignKey("Gender")]
            public int genderId { get; set; }
            public virtual Gender? Gender { get; set; }


            [Required(ErrorMessage = "Designation is required")]
            [ForeignKey("Designation")]
            public int designationId { get; set; }
            public virtual Designation? Designation { get; set; }

            [Required]
            [MaxLength(255)]
            public string employeePassword { get; set; }

            [Required]
            [MaxLength(255)]
            public string employeeEmail { get; set; } = string.Empty;

            public DateOnly dateOfJoining { get; set; }
            public DateOnly dateOfBirth { get; set; }


            public bool isActive { get; set; }

       
        }
}
