﻿using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;

namespace leaveApplication2.Repostories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public EmployeeRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IReadOnlyCollection<Employee>> GetEmployeesAsync()
        {
            // return await _context.Employees.ToListAsync();
            return await _context.Employees.Include(e => e.Designation).Include(e => e.Gender).OrderBy(h => h.dateOfJoining).AsNoTracking().ToListAsync();

        }





        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            
            var singleEmployee = await _context.Employees.Include(e => e.Designation).Include(e => e.Gender).AsNoTracking().FirstOrDefaultAsync(e => e.employeeId == id);
           
            if (singleEmployee == null) 
            {
                return null;
            }
            
            return singleEmployee;
        }


        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Expression<Func<Employee, bool>> filter)
        {
            return await _context.Employees.Where(filter).ToListAsync();
        }


        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
            
        }

        public async Task<Employee> RegisterEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }


        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or take appropriate action
                // For example, you can rethrow the exception, return a default value, or handle it gracefully
                // Logging the exception is a good practice to help with debugging
                // Example: _logger.LogError(ex, "An error occurred while updating the applied leave.");

                throw ex; // Rethrow the exception to propagate it up the call stack
            }
        }



        public async Task DeleteEmployeeAsync(long id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.SingleOrDefaultAsync(e => e.emailAddress == email);
        }

        public async Task<Employee> EmployeeLoginAsync(Employee  employee)
        {
            try
            {
                return await _context.Employees.SingleOrDefaultAsync(e => e.emailAddress == employee.emailAddress && e.employeePassword == employee.employeePassword);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> VerifyEmployeeEmailAsync(string employeeEmail)
        {
            // Check if the email already exists in the database

            var existingEmployee = await _context.Employees
                .Where(e => e.emailAddress == employeeEmail)
                .FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                return existingEmployee.emailAddress;
            }

            return null; // Email doesn't exist
        }
        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _context.Database.BeginTransaction(isolationLevel);
        }

        public async Task<EmployeeReporting> CreateEmployeeReportingAsync(long employeeId, long reportingPersonId)
        {
            var existingRecord = await _context.EmployeeReporting
        .Where(er => er.EmployeeId == employeeId && er.ReportingPersonId == reportingPersonId)
        .FirstOrDefaultAsync();

            if (existingRecord != null)
            {
                // A record with the same combination already exists. You can return an error here.
                throw new Exception("A record with the same combination of employeeId and reportingPersonId already exists.");
            }

            // If no matching record exists, create a new EmployeeReporting record.
            var employeeReporting = new EmployeeReporting
            {
                EmployeeId = employeeId,
                ReportingPersonId = reportingPersonId
            };

            _context.EmployeeReporting.Add(employeeReporting);
            await _context.SaveChangesAsync();

            return employeeReporting;
        }


    }
}
