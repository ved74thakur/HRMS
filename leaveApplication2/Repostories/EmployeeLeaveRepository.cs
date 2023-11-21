using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly ApplicationDbContext _context;
       // private readonly IEmailService  _emailService;
        public EmployeeLeaveRepository(ApplicationDbContext context)
        {
            _context = context;
           // _emailService = emailService;

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
        public async Task<EmployeeLeave> UpdateEmployeeLeaveAsync(EmployeeLeave employeeLeave)
        {

            if (employeeLeave != null && _context.Entry(employeeLeave).State == EntityState.Detached)
            {
                _context.Entry(employeeLeave.Employee).State = EntityState.Detached;
             //   _context.Attach(employeeLeave);
            }

            _context.EmployeeLeaves.Update(employeeLeave);
           
            await _context.SaveChangesAsync();

           
          _context.Entry(employeeLeave.Employee).State = EntityState.Detached;
              
            

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
        public async Task<EmployeeLeave> GetEmployeeLeaveAsync(Expression<Func<EmployeeLeave, bool>> filter)
        {
            try
            {
                var employeeLeave  =  await _context.EmployeeLeaves.AsNoTracking().Include(e => e.Employee).Include(e => e.LeaveType).Include(e => e.LeaveAllocation).FirstOrDefaultAsync(filter);


                _context.Entry(employeeLeave.Employee).State = EntityState.Detached;
                return employeeLeave;
            }
            catch (Exception ex)
            {
               // await _emailService.SendErrorMail("ved.thakur@wonderbiz.in", ex.Message, "GetEmployeeLeaveAsync");
                throw;
            }
        }
    }
}
