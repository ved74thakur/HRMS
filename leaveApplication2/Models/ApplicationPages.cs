﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Models
{
    [Table("ApplicationPages")]
    public class ApplicationPages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
