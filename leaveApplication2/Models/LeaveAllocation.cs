using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("LeaveAllocations")]
    public class LeaveAllocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveAllocationId { get; set; }

        public int financialYearId { get; set; }
        [ForeignKey("financialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }

        public int leaveTypeId { get; set; }
        [ForeignKey("leaveTypeId")]
        public virtual LeaveType LeaveType { get; set; }

        [Required(ErrorMessage = "Leave count is required")]
        public int leaveCount { get; set; }

     
    }
}
