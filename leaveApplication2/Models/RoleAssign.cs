using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("RoleAssign")]
    public class RoleAssign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleAssignId { get; set; }
        public string RoleAssignName { get; set; }
        public string RoleAssignCodeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; } = false;
    }
}
