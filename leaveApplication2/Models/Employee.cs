using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string gender { get; set; } = string.Empty;


        [Required(ErrorMessage = "Designation is required")]
        [ForeignKey("Designation")]
        public int designationId { get; set; }
        public virtual Designation? Designation { get; set; }


        public bool isActive { get; set; }
       
    }
}
