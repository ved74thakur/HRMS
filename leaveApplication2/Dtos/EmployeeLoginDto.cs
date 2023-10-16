using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Dtos
{
    public class EmployeeLoginDto
    {
        public long employeeId { get; set; }
        public string? email { get; set; }

        public string? password { get; set; }
    }
}
