using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leaveApplication2.Models
{
    public class PolicyDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int policyDocumentId {  get; set; }
        
        public string policyName { get; set; }

        public byte[] content { get; set; }

        public bool isActive { get; set; }

    }
}
