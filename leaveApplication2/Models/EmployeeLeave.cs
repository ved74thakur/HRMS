using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("EmployeeLeaves")]
    public class EmployeeLeave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long employeeLeaveId { get; set; }

        [ForeignKey("Employee")]
        public long employeeId { get; set; }
        public virtual Employee? Employee { get; set; } 

        [ForeignKey("LeaveType")]
        public int leaveTypeId { get; set; }
        public virtual LeaveType? LeaveType { get; set; }

        [Required(ErrorMessage = "LeaveCount is required")]
        public int leaveCount { get; set; }

        [Required(ErrorMessage = "Consumed leaves is required")]
        public int consumedLeaves { get; set; } 

        [Required(ErrorMessage = "Balance leaves is required")]
        
       
        public int balanceLeaves { get; set; }

        public bool isActive { get; set; } = false;

    }
}
