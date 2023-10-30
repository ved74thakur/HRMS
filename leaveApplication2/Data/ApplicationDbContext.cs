using System.Reflection.Emit;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
        //public DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public DbSet<Test> Tests { get; set; }

        public DbSet<Gender>  Genders { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<FinancialYear> FinancialYears { get; set; }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<RoleAssign> RoleAssigns { get; set; }
        public DbSet<ApplicationPage> ApplicationPages { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        public DbSet<EmployeeReporting> EmployeeReporting { get; set; }
        //public DbSet<Test2> Test2s { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.Entity<Employee>()
               .HasIndex(u => u.emailAddress)
            .IsUnique();
            // Configure the relationship between Employee and EmployeeReporting
            /* builder.Entity<EmployeeReporting>()
                .HasOne(er => er.EmployeeId)
                .WithMany()
                .HasForeignKey(er => er.EmployeeId);

            builder.Entity<EmployeeReporting>()
                .HasOne(er => er.ReportingPersonId)
                .WithMany()
                .HasForeignKey(er => er.ReportingPersonId); */


            /*   builder.Entity<Employee>().ToTable("employeeReporting");
              builder.Entity<Employee>().HasKey(e => new { e.employeeId, e.ReportingPersonId }); */


            // Configure the relationship between UserRoleMapping, ApplicationPages, and RoleAssign
            /* builder.Entity<UserRoleMapping>()
                .HasOne(mapping => mapping.ApplicationPages)
                .WithMany()
                .HasForeignKey(mapping => mapping.ApplicationPageId);

            builder.Entity<UserRoleMapping>()
                .HasOne(mapping => mapping.RoleAssignment)
                .WithMany()
                .HasForeignKey(mapping => mapping.RoleAssignId); */
        }

    }
}
