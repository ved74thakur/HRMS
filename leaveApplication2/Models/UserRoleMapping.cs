using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("UserRoleMapping")]
    public class UserRoleMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign key to ApplicationPages
        [ForeignKey("ApplicationPage")]
        public int ApplicationPageId { get; set; }
        public virtual ApplicationPages ApplicationPage { get; set; }

        // Foreign key to RoleAssign
        [ForeignKey("RoleAssignment")]
        public int RoleAssignId { get; set; }
        public virtual RoleAssign RoleAssignment { get; set; }


    }
}
