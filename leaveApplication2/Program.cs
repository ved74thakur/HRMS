
using leaveApplication2.Data;
using leaveApplication2.Repostories;
using leaveApplication2.Services;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("postgreSQLConnection");

        //Add services to the container
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IAppliedLeaveRepository, AppliedLeaveRepository>();
        builder.Services.AddScoped<IAppliedLeaveService, AppliedLeaveService>();
        builder.Services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
        builder.Services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
        builder.Services.AddScoped<ILeaveStatusRepository, LeaveStatusRepository>();
        builder.Services.AddScoped<ILeaveStatusService, LeaveStatusService>();

        //ILeaveTypeRepository
        builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", builder =>
            {
                builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers();
        

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseCors("EnableCORS");

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors();

        app.MapGet("/", () => "Hello World!");

        
        app.Run();
    }
}