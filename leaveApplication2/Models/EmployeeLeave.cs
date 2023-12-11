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
        public double leaveCount { get; set; }

        [Required(ErrorMessage = "Consumed leaves is required")]
        public double consumedLeaves { get; set; } 

        [Required(ErrorMessage = "Balance leaves is required")]
        
       
        public double balanceLeaves { get; set; }

        public double carryForward {  get; set; }

        public double adjustmentAdd { get; set; }
        public double adjustmentDel {  get; set; }


        [ForeignKey("LeaveAllocation")]
        public int leaveAllocationId { get; set; }

        public virtual LeaveAllocation? LeaveAllocation { get; set; }


        public bool isActive { get; set; } = false;


       //this
      

    }
}
