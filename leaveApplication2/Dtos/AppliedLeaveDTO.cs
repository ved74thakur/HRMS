namespace leaveApplication2.Dtos
{
    public class AppliedLeaveDTO
    {
        public long appliedLeaveTypeId { get; set; }
        public long employeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;
        public double BalanceLeave { get; set; }
        public double AppliedLeave { get; set; }
        public double RemaingLeave { get; set; }
        public string LeaveReason { get; set; } = string.Empty;
        public double ApplyLeaveDay { get; set; }
        public bool isApproved { get; set; }

        public bool isRejected { get; set; }

        public string LeaveStatusName { get; set; } = string.Empty;

        public string LeaveStatusCode { get; set; } = string.Empty;


    }
}