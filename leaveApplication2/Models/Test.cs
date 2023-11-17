using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long employeeId { get; set; }
        public string employeeCode { get; set; } = string.Empty;

        public string description { get; set; }

        public string employeeName { get; set; }

        [Required]
        [MaxLength(255)]
        public string firstName { get; set; } = string.Empty;

        public string lastName { get; set; }
    }
}
