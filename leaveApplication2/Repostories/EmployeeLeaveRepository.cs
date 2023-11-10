using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeLeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<EmployeeLeave>> GetAllEmployeesLeaveAsync()
        {
            return await _context.EmployeeLeaves.ToListAsync();
            //return await _context.Employees.Include(e => e.Designation).AsNoTracking().ToListAsync();
        }



        public async Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave)
        {
            _context.EmployeeLeaves.Add(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id)
        {
            var singleEmployeeLeave = await _context.EmployeeLeaves.FindAsync(id);
            if (singleEmployeeLeave == null)
            {
                return null;
            }
            return singleEmployeeLeave;
        }

        public async Task<List<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId)
        {
            var employeeLeaves = await _context.EmployeeLeaves
        .Where(e => e.employeeId == employeeId && e.isActive)
        .ToListAsync();

            return employeeLeaves;
        }

        //update

        public async Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave)
      
        {


            _context.EmployeeLeaves.Update(employeeLeave);
            await _context.SaveChangesAsync();
            return employeeLeave;

        }

        public async Task<EmployeeLeave> SetEmployeeLeaveToFalseAsync(long id)
        {
            //error
            var employee = await _context.EmployeeLeaves.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Leave not found");
            }

            employee.isActive = false;
            await _context.SaveChangesAsync();
            return employee;
        }
        
        public async Task<EmployeeLeave> GetEmployeeLeaveByEmployee(long employeeId, int leaveTypeId)
        {
            // Assuming you have a DbContext called 'dbContext' with a DbSet for 'EmployeeLeave'
            // Replace 'EmployeeLeave' and 'dbContext' with your actual entity and context names.

            var employeeLeave = await _context.EmployeeLeaves
                .Where(el => el.employeeId == employeeId && el.leaveTypeId == leaveTypeId)
                .FirstOrDefaultAsync();

            if (employeeLeave == null)
            {
                // Handle the case where the employee leave is not found, e.g., return NotFound or null.
                // You can throw an exception or return a specific result as needed.
                return null; // You can modify this to return NotFound or throw an exception.
            }

            return employeeLeave;
        }

    }
}
