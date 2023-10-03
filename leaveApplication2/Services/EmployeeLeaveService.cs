using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {

        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;

        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
        }



        public async Task<IEnumerable<EmployeeLeave>> GetAllEmployeesLeaveAsync()
        {
            return await _employeeLeaveRepository.GetAllEmployeesLeaveAsync();
        }

        public async Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave)
        {

            var createdLeave = await _employeeLeaveRepository.CreateEmployeeLeaveAsync(leave);
            return createdLeave;
        }

        public async Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id)
        {
            var singleEmployeeLeave = await _employeeLeaveRepository.GetEmployeeLeaveByIdAsync(id);
            return singleEmployeeLeave;
        }

        public async Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId)
        {
            return await _employeeLeaveRepository.GetEmployeeLeaveByEmployeeId(employeeId);
        }
    }

}
