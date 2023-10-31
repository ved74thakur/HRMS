using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("ApplicationPage")]
    public class ApplicationPages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public bool IsActive { get; set; }

        public string routePath { get; set; }
        public string menuPath { get; set; }
        public bool isMenuPage { get; set; } = true;
        public string componentName { get; set; } = string.Empty;
    }
}
