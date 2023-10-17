
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveapplication2.models
{
    [Table("leavestatuses")]
    public class LeaveStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveStatusId { get; set; }

        [Required]
        public string LeaveStatusName { get; set; } = string.Empty;

        public string LeaveStatusNameCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
