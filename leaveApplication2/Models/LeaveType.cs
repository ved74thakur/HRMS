using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("LeaveTypes")]  
    public class LeaveType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int leaveTypeId { get; set; }

        [Required(ErrorMessage = "Type of leaves is required")]
        [MaxLength(255)]
        public string leaveTypeName { get; set; } = string.Empty;
        public string leaveTypeNameCode { get; set; } = string.Empty;
        public bool isActive { get; set; } = false;


    }
}
