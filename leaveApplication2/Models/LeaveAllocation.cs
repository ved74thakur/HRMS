using leaveApplication2.Models.leaveApplication2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
   [Table("LeaveAllocations")]
    public class LeaveAllocation
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveAllocationId { get; set; }

        [Required]
        [ForeignKey("FinancialYear")]
        public int financialYearId { get; set; }
   
        public virtual FinancialYear? FinancialYear { get; set; }

        [ForeignKey("LeaveType")]
        public int leaveTypeId { get; set; }
  
        public virtual LeaveType? LeaveType { get; set; }

       [Required(ErrorMessage = "Leave count is required")]
        public int leaveCount { get; set; }


    }

}
