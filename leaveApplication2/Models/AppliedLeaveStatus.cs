using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    public class AppliedLeaveStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long appliedLeaveStatusId { get; set; }


        [ForeignKey("AppliedLeave")]
        public long appliedLeaveTypeId { get; set; }
        public virtual AppliedLeave? AppliedLeave { get; set; }

        public bool approve { get; set; }
        public bool reject { get; set; }
        public bool onHold { get; set; }
        public bool isActive { get; set; } = false;
    }
}
