using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace leaveApplication2.Models
{
    
    [Table("Designations")]
    public class Designation
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int designationId { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public string designationName { get; set; } = string.Empty;
        public string designationCode { get; set; } = string.Empty;
        public bool isActive { get; set; }
    }
}
