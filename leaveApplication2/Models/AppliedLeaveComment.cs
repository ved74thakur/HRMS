using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("ApplicationPage")]
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



    }
}
