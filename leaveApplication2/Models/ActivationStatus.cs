using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("ActivationStatuses")]
    public class ActivationStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int activationStatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string activationStatus { get; set; }
    }
}
