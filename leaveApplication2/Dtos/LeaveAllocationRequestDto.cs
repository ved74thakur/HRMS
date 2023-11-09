using leaveApplication2.Models.leaveApplication2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Dtos
{
    public class LeaveAllocationRequestDto
    {
        public FinancialYear FinancialYear { get; set; }
        public Dictionary<int, int> LeaveTypeCounts { get; set; }
    }
    
}
