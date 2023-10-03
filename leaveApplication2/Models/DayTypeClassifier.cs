using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("DayTypeClassifiers")]
    public class DayTypeClassifier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long dayTypeClassiferId { get; set; }

        [Required]
        public string DayTypeClassifierName { get; set; }
        public bool IsActive { get; set; }
        public string DayTypeClassifierNameCode { get; set; }
    }

}

