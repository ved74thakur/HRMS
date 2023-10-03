using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    public class EmailModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int emailId { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        [EmailAddress]
        public string RecipientEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MinLength(10)] // Adjust the minimum length as needed
        public string Body { get; set; }
    }
}
