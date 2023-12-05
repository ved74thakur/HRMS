using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("Holidays")]
    public class Holiday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "HolidayName is required.")]

        public string HolidayName { get; set; }

        [Required(ErrorMessage = "Holiday Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly HolidayDate { get; set; }


        public bool IsActive { get; set; }
    }
}
