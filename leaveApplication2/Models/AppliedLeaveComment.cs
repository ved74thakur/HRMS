using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("AppliedLeaveComments")]
    public class AppliedLeaveComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long appliedLeaveCommentId { get; set; }

        [ForeignKey("AppliedLeave")]
        public long appliedLeaveTypeId { get; set; }
        public virtual AppliedLeave? AppliedLeave { get; set; }

        [ForeignKey("LeaveStatus")]
        public int LeaveStatusId { get; set; }
        public virtual LeaveStatus? LeaveStatus { get; set; }

        [StringLength(256, ErrorMessage = "Leave Reason cannot exceed 256 characters.")]
        public string? comment { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss.fffZ}")]
        public DateTime date { get; set; }

        [ForeignKey("Employee")]
        public long createdEmpId { get; set; }
        public virtual Employee? Employee { get; set; }


    }
}
