using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("AppliedLeaves")]
    public class AppliedLeave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long appliedLeaveTypeId { get; set; }
        //employee id

        [ForeignKey("Employee")]
        public long employeeId { get; set; }
        public virtual Employee? Employee { get; set; } 

        [ForeignKey("LeaveType")]
        public int leaveTypeId { get; set; }
        public virtual LeaveType? LeaveType { get; set; }

        


        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss.fffZ}")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss.fffZ}")]
        public DateTime EndDate { get; set; }


        [StringLength(256, ErrorMessage = "Leave Reason cannot exceed 256 characters.")]
        public string? LeaveReason { get; set; }

        //number of days leave taken
        [Range(0, int.MaxValue, ErrorMessage = "NumberOFDays  must be a non-negative number.")]
        public int applyLeaveDay { get; set; } = 0;

        [ForeignKey("LeaveStatus")]
        public int leaveStatusId { get; set; } 
        public virtual LeaveStatus? LeaveStatus { get; set; }






        [Range(0, int.MaxValue, ErrorMessage = "Difference must be a non-negative number.")]
        public int remaingLeave { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Balance Leave must be a non-negative number.")]
        public int balanceLeave { get; set; }


        //[ForeignKey("employeeLeaveId")]
        //public int employeeLeaveId { get; set; }
        //public virtual EmployeeLeave? balanceLeaves { get; set; }

    }
}
