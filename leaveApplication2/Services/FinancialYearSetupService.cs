using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace leaveApplication2.Services
{
    public class FinancialYearSetupService : IFinancialYearSetupService
    {
        //Hear you have to call that 
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IEmployeeService _employeeService;
        
        

        
        public FinancialYearSetupService(ILeaveAllocationService leaveAllocationService, IEmployeeLeaveService employeeLeaveService, IEmployeeService employeeService)
        {

            _leaveAllocationService = leaveAllocationService;
            _employeeLeaveService = employeeLeaveService;
            _employeeService = employeeService;
            

        }

        public async Task<string> CreateUpdatedEmployeeLeaveAsync()
        {
            var employeeLeaves = await _employeeLeaveService.GetAllEmployeesLeaveAsync();
            
            foreach( var empLv in employeeLeaves)
            {
                await _employeeLeaveService.SetEmployeeLeaveToFalseAsync(empLv.employeeLeaveId);
            }
            IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _leaveAllocationService.GetLeaveAllocationsAsync();
            foreach (var leaveAllocation in leaveAllocations)
            {
                var employees = await _employeeService.GetEmployeesAsync();
                foreach (var employee in employees)
                {
                    var leave = new EmployeeLeave
                    {
                        employeeId = employee.employeeId, // Set the appropriate employeeId
                        leaveTypeId = leaveAllocation.leaveTypeId,
                        leaveCount = leaveAllocation.leaveCount,
                        consumedLeaves = 0, // Initialize consumedLeaves as required
                        balanceLeaves = leaveAllocation.leaveCount, // Initialize balanceLeaves as the leaveCount from LeaveAllocation
                        isActive = true, // Set isActive as required
                        leaveAllocationId = leaveAllocation.leaveAllocationId
                    };

                    // Insert the created EmployeeLeave object into your repository
                    var createdLeave = await _employeeLeaveService.CreateEmployeeLeaveAsync(leave);

                }

            }

            return "Success";
           

        }
    }
}
