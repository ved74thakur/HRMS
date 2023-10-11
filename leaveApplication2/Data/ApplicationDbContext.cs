﻿using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<AppliedLeave> AppliedLeaves { get; set; }

        public DbSet<EmailModel> EmailModels { get; set; }
        public DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public DbSet<ActivationStatus> ActivationStatuses { get; set; }
        public DbSet<Test> Tests { get; set; }
        
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
