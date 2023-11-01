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
    [Migration("20231011034641_pralhadisGreat")]
    partial class pralhadisGreat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("leaveApplication2.Models.ActivationStatus", b =>
                {
                    b.Property<int>("activationStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("activationStatusId"));

                    b.Property<string>("activationStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("activationStatusId");

                    b.ToTable("ActivationStatuses");
                });

            modelBuilder.Entity("leaveApplication2.Models.AppliedLeave", b =>
                {
                    b.Property<long>("appliedLeaveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("appliedLeaveTypeId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LeaveReason")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("applyLeaveDay")
                        .HasColumnType("integer");

                    b.Property<int>("balanceLeave")
                        .HasColumnType("integer");

                    b.Property<long>("employeeId")
                        .HasColumnType("bigint");

                    b.Property<int>("leaveStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("leaveTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("remaingLeave")
                        .HasColumnType("integer");

                    b.HasKey("appliedLeaveTypeId");

                    b.HasIndex("employeeId");

                    b.HasIndex("leaveStatusId");

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

                    b.Property<int>("activationStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("designationId")
                        .HasColumnType("integer");

                    b.Property<string>("employeeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("employeeEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("employeeId");

                    b.HasIndex("activationStatusId");

                    b.HasIndex("designationId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("leaveApplication2.Models.EmployeeLeave", b =>
                {
                    b.Property<long>("employeeLeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("employeeLeaveId"));

                    b.Property<int>("balanceLeaves")
                        .HasColumnType("integer");

                    b.Property<int>("consumedLeaves")
                        .HasColumnType("integer");

                    b.Property<long>("employeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<int>("leaveCount")
                        .HasColumnType("integer");

                    b.Property<int>("leaveTypeId")
                        .HasColumnType("integer");

                    b.HasKey("employeeLeaveId");

                    b.HasIndex("employeeId");

                    b.HasIndex("leaveTypeId");

                    b.ToTable("EmployeeLeaves");
                });

            modelBuilder.Entity("leaveApplication2.Models.EmployeeRegistration", b =>
                {
                    b.Property<long>("employeeRegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("employeeRegistrationId"));

                    b.Property<string>("employeeEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("employeeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("employeeRegistrationId");

                    b.ToTable("EmployeeRegistrations");
                });

            modelBuilder.Entity("leaveApplication2.Models.LeaveStatus", b =>
                {
                    b.Property<int>("leaveStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("leaveStatusId"));

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("leaveStatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("leaveStatusNameCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("leaveStatusId");

                    b.ToTable("LeaveStatuses");
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

            modelBuilder.Entity("leaveApplication2.Models.AppliedLeave", b =>
                {
                    b.HasOne("leaveApplication2.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.LeaveStatus", "LeaveStatus")
                        .WithMany()
                        .HasForeignKey("leaveStatusId")
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
                    b.HasOne("leaveApplication2.Models.ActivationStatus", "ActivationStatus")
                        .WithMany()
                        .HasForeignKey("activationStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("leaveApplication2.Models.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("designationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivationStatus");

                    b.Navigation("Designation");
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
#pragma warning restore 612, 618
        }
    }
}
