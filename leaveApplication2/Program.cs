
using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("postgreSQLConnection");
        var secretKey = builder.Configuration.GetSection("Jwt")["Secret"];
        //var smtpSettings = builder.Configuration.GetSection("SmtpSettings");

        //Add services to the container
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        //builder.Services.Configure<SmtpSettings>(smtpSettings);
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IAppliedLeaveRepository, AppliedLeaveRepository>();
        builder.Services.AddScoped<IAppliedLeaveService, AppliedLeaveService>();
        builder.Services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
        builder.Services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
        builder.Services.AddScoped<ILeaveStatusRepository, LeaveStatusRepository>();
        builder.Services.AddScoped<ILeaveStatusService, LeaveStatusService>();
        
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

        //ILeaveTypeRepository
        builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
                };
            });


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

        
        app.UseCors("EnableCORS");

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors();

        app.MapGet("/", () => "Hello World!");

        
        app.Run();
    }
}