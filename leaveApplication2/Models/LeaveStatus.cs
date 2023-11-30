using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("LeaveStatuses")]
    public class LeaveStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveStatusId { get; set; }

        [Required]
        public string LeaveStatusName { get; set; } = string.Empty;

        public string LeaveStatusCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
