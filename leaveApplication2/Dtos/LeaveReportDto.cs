namespace leaveApplication2.Dtos
{
    public class LeaveReportDto
    {
 
        public record LeaveReport(DateTime startDate, DateTime endDate, long employeeId, long LeaveStatusId);
          
    }
}
