using leaveApplication2.Dtos;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _config;

        public AuthService(IEmployeeRepository employeeRepository, IConfiguration config)
        {
            _employeeRepository = employeeRepository;
            _config = config;
        }

        
       


    }
}
