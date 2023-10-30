using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    [Table("EmployeeReporting")]
    public class EmployeeReporting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeReportingId { get; set; }

        //[ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        //public virtual Employee? Employee { get; set; }

        //[ForeignKey("Employee")]
        public long ReportingPersonId { get; set; }
        //public virtual Employee? Employee { get; set; } 
    }
}
