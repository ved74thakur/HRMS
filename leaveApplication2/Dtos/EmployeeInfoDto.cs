namespace leaveApplication2.Dtos
{
    public class EmployeeInfoDto
    {
        public long employeeId {  get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        public string emailAddress { get; set; } = string.Empty;
        public string mobileNo { get; set; } = string.Empty;
        public string? designationName { get; set; }

        public DateOnly dateOfJoining { get; set; }
        public DateOnly dateOfBirth { get; set; }

        public string reportingPersonFirstName { get; set; } = string.Empty;
        public string reportingPersonLastName { get; set; } = string.Empty;
    }
}
