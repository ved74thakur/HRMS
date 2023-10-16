using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("Genders")]
    public class Gender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int genderId { get; set; }

        public string genderCode { get; set; } =string.Empty;

        [Required]
        public string genderName { get; set; } = string.Empty;

        public bool isActive { get; set; } = true;

    }
}
