
using Leave.EmailTemplate;
using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
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
        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseNpgsql(connectionString);
        //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //    );

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });


        //builder.Services.Configure<SmtpSettings>(smtpSettings);
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IAppliedLeaveRepository, AppliedLeaveRepository>();
        builder.Services.AddScoped<IAppliedLeaveService, AppliedLeaveService>();
        builder.Services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
        builder.Services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
        //builder.Services.AddScoped<ILeaveStatusRepository, LeaveStatusRepository>();
        builder.Services.AddScoped<IGenderRepository, GenderRepository>();
        builder.Services.AddScoped<IGenderService, GenderService>();
        builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IDesignationService, DesignationService>();
        builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
        builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
        builder.Services.AddScoped<IHolidayService, HolidayService>();
        builder.Services.AddScoped<IRoleAssignRepository, RoleAssignRepository>();
        builder.Services.AddScoped<IRoleAssignService, RoleAssignService>();
        builder.Services.AddScoped<IApplicationPageRepository, ApplicationPageRepository>();
        builder.Services.AddScoped<IApplicationPageServices, ApplicationPageServices>();
        builder.Services.AddScoped<IUserRoleMappingRepository, UserRoleMappingRepository>();
        builder.Services.AddScoped<IUserRoleMappingServices, UserRoleMappingServices>();
        builder.Services.AddScoped<IFinancialYearService, FinancialYearService>();
        builder.Services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
        builder.Services.AddScoped<IFinancialYearSetupService, FinancialYearSetupService>();
        builder.Services.AddScoped<IPolicyDocumentRepository, PolicyDocumentRepository>();
        builder.Services.AddScoped<IPolicyDocumentService, PolicyDocumentService>();
        builder.Services.AddScoped<ILeaveReportService, LeaveReportService>();


        builder.Services.AddScoped<ILeaveStatusService, LeaveStatusService>();
        builder.Services.AddScoped<ILeaveStatusRepository, LeaveStatusRepository>();




        builder.Services.AddScoped<IAuthService, AuthService>();



        //builder.Services.AddScoped<ILeaveStatusService, LeaveStatusService>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        
        builder.Services.AddTransient<GenericEmail>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //ILeaveTypeRepository
        builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        builder.Services.AddAuthorization();
        
       

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"], // Replace with your issuer
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"], // Replace with "JWTServicePostmanClient"
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])), // Replace with your secret key
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

      

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("EnableCORS");
        app.MapControllers();

  //      app.UseCors();

        app.MapGet("/", () => "Hello World!");
   /*Test*/

        app.Run();
    }
}