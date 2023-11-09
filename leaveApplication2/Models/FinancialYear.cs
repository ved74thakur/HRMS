using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    namespace leaveApplication2.Models
    {
        [Table("FinancialYears")]
        public class FinancialYear
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int financialYearId { get; set; }

            [Required(ErrorMessage = "Start date is required")]
            public DateOnly startDate { get; set; }

            [Required(ErrorMessage = "End date is required")]
            public DateOnly endDate { get; set; }

            public bool ActiveYear { get; set; } = true;
        }
    }
}
