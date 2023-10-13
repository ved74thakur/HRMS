using System.ComponentModel.DataAnnotations;

namespace leaveApplication2.Dtos
{
    public class EmployeeLoginDto
    {
       
        public string? email { get; set; }

        public string? password { get; set; }
    }
}
