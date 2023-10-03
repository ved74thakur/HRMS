using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {

        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly ILeaveStatusRepository _leaveStatusRepository;

        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository, ILeaveStatusRepository leaveStatusRepository)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
            _leaveStatusRepository = leaveStatusRepository;

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

        public async Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave)
        {

            var updateEmployeeLeave = await _employeeLeaveRepository.UpdateEmployeeLeaveAsync(id, employeeLeave);
            return updateEmployeeLeave;

        }
        //UPDATE EMPLOYEE LEAVE BASED ON STATUSCODE FOR APPROVE

        public async Task<EmployeeLeave> UpdateEmployeeLeaveIfAPV(long id, string leaveStatusNameCode, EmployeeLeave employeeLeave)
        {
            var leaveStatusCode = await _leaveStatusRepository.GetLeaveStatusByCodeAsync(leaveStatusNameCode);
            if (leaveStatusCode == null)
            {
                return null;
            }

            else if (leaveStatusCode.leaveStatusNameCode == "APV")
            {
                return await UpdateEmployeeLeaveAsync(id, employeeLeave);
            }

            return null;
        }
        


    }



}
