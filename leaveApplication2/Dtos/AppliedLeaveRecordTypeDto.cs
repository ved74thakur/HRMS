namespace leaveApplication2.Dtos
{
    public record AppliedLeaveUpdateStatus(long appliedLeaveTypeId, string statusCode, long leaveAllocationId, string? commentByUser, DateTime? date);

}
