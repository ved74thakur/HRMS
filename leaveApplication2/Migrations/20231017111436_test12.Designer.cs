﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using leaveApplication2.Data;

#nullable disable

namespace leaveApplication2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231017111436_test12")]
    partial class test12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("leaveApplication2.Models.AppliedLeave", b =>
                {
                    b.Property<long>("appliedLeaveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("appliedLeaveTypeId"));

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ApprovedNotes")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsHalfDay")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("boolean");

                    b.Property<string>("LeaveReason")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("LeaveStatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RejectedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RejectedNotes")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("applyLeaveDay")
                        .HasColumnType("double precision");

                    b.Property<double>("balanceLeave")
                        .HasColumnType("double precision");

                    b.Property<long>("employeeId")
                        .HasColumnType("bigint");

                    b.Property<int>("leaveTypeId")
                        .HasColumnType("integer");

                    b.Property<double>("remaingLeave")
                        .HasColumnType("double precision");

                    b.HasKey("appliedLeaveTypeId");

                    b.HasIndex("LeaveStatusId");

                    b.HasIndex("employeeId");

                    b.HasIndex("leaveTypeId");

                    b.ToTable("AppliedLeaves");
                });

            modelBuilder.Entity("leaveApplication2.Models.Designation", b =>
                {
                    b.Property<int>("designationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("designationId"));

                    b.Property<string>("designationCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("designationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("designationId");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("leaveApplication2.Models.EmailModel", b =>
                {
                    b.Property<int>("emailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("emailId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("emailId");

                    b.ToTable("EmailModels");
                });

            modelBuilder.Entity("leaveApplication2.Models.Employee", b =>
                {
                    b.Property<long>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("employeeId"));

                    b.Property<DateOnly>("dateOfBirth")
                        .HasColumnType("date");

                    b.Property<DateOnly>("dateOfJoining")
                        .HasColumnType("date");

                    b.Property<int>("designationId")
                        .HasColumnType("integer");

                    b.Property<string>("emailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("employeeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("employeePassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("genderId")
                        .HasColumnType("integer");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("mobileNo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("employeeId");

                    b.HasIndex("designationId");

                    b.HasIndex("emailAddress")
                        .IsUnique();

                    b.HasIndex("genderId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("leaveApplication2.Models.EmployeeLeave", b =>
                {
                    b.Property<long>("employeeLeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("employeeLeaveId"));

                    b.Property<double>("balanceLeaves")
                        .HasColumnType("double precision");

                    b.Property<double>("consumedLeaves")
                        .HasColumnType("double precision");

                    b.Property<long>("employeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<double>("leaveCount")
                        .HasColumnType("double precision");

                    b.Property<int>("leaveTypeId")
                        .HasColumnType("integer");

                    b.HasKey("employeeLeaveId");

                    b.HasIndex("employeeId");

                    b.HasIndex("leaveTypeId");

                    b.ToTable("EmployeeLeaves");
                });

            modelBuilder.Entity("leaveApplication2.Models.Gender", b =>
                {
                    b.Property<int>("genderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("genderId"));

                    b.Property<string>("genderCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("genderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("genderId");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("leaveApplication2.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("HolidayDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HolidayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("leaveApplication2.Models.LeaveAllocation", b =>
                {
                    b.Property<int>("leaveAllocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("leaveAllocationId"));

                    b.Property<int>("financialYearId")
                        .HasColumnType("integer");

                    b.Property<int>("leaveCount")
                        .HasColumnType("integer");

                    b.Property<int>("leaveTypeId")
                        .HasColumnType("integer");

                    b.HasKey("leaveAllocationId");

                    b.HasIndex("financialYearId");

                    b.HasIndex("leaveTypeId");

                    b.ToTable("LeaveAllocations");
                });

            modelBuilder.Entity("leaveApplication2.Models.LeaveType", b =>
                {
                    b.Property<int>("leaveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("leaveTypeId"));

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("leaveTypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("leaveTypeNameCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("leaveTypeId");

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("leaveApplication2.Models.Test", b =>
                {
                    b.Property<long>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("employeeId"));

                    b.Property<string>("employeeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("employeeId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("leaveApplication2.Models.leaveApplication2.Models.FinancialYear", b =>
                {
                    b.Property<int>("financialYearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("financialYearId"));

                    b.Property<bool>("ActiveYear")
                        .HasColumnType("boolean");

                    b.Property<DateOnly>("endDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("startDate")
                        .HasColumnType("date");

                    b.HasKey("financialYearId");

                    b.ToTable("FinancialYears");
                });

            modelBuilder.Entity("leaveapplication2.models.LeaveStatus", b =>
                {
                    b.Property<int>("LeaveStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LeaveStatusId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LeaveStatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LeaveStatusNameCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LeaveStatusId");

                    b.ToTable("leavestatuses");
                });

            modelBuilder.Entity("leaveApplication2.Models.AppliedLeave", b =>
                {
                    b.HasOne("leaveapplication2.models.LeaveStatus", "LeaveStatus")
                        .WithMany()
                        .HasForeignKey("LeaveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("leaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveStatus");

                    b.Navigation("LeaveType");
                });

            modelBuilder.Entity("leaveApplication2.Models.Employee", b =>
                {
                    b.HasOne("leaveApplication2.Models.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("designationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("genderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designation");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("leaveApplication2.Models.EmployeeLeave", b =>
                {
                    b.HasOne("leaveApplication2.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("leaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveType");
                });

            modelBuilder.Entity("leaveApplication2.Models.LeaveAllocation", b =>
                {
                    b.HasOne("leaveApplication2.Models.leaveApplication2.Models.FinancialYear", "FinancialYear")
                        .WithMany()
                        .HasForeignKey("financialYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("leaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FinancialYear");

                    b.Navigation("LeaveType");
                });
#pragma warning restore 612, 618
        }
    }
}
