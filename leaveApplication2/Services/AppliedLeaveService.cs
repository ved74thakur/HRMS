﻿using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Other;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace leaveApplication2.Services
{
    public class AppliedLeaveService : IAppliedLeaveService
    {
        private readonly IAppliedLeaveRepository _leaveRepository;
        private readonly ILeaveStatusRepository _leaveStatusRepository;
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;

        private readonly ILeaveStatusService  _leaveStatusService;
        private readonly IEmailService _emailService; 

        public AppliedLeaveService(IAppliedLeaveRepository leaveRepository,  IEmployeeLeaveRepository employeeLeaveRepository, ILeaveStatusService leaveStatusService, IEmailService emailService)
        {
            _leaveRepository = leaveRepository;
           
            _employeeLeaveRepository = employeeLeaveRepository;
            _leaveStatusService = leaveStatusService;
            _emailService = emailService;
        }

        public async Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync()
        {
            return await _leaveRepository.GetAppliedLeavesAsync();

        }
       
        public async Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave)
        {

            try
            {
                var leaveStatus = await _leaveStatusService.GetLeaveStatusByCodeAsync("APP");




                if (leaveStatus == null)
                {
                    throw new ArgumentNullException(nameof(leaveStatus), "Leave status not found. (APP)");
                }
                leave.LeaveStatusId = leaveStatus.LeaveStatusId;

                var createdLeave = await _leaveRepository.CreateAppliedLeave(leave);

               

                return createdLeave;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //END POINT
        //public async Task<AppliedLeave> UpdateLeaveStatusAsync(long appliedLeaveTypeId , int leaveStatusId)
        //{
        //    var singleLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
        //    if (singleLeave == null)
        //    {
        //        return null;
        //    } 
        //    singleLeave.leaveStatusId = leaveStatusId;
        //    var updateLeave = await _leaveRepository.UpdateAppliedLeaveAsync(appliedLeaveTypeId, singleLeave);
        //    var leaveStatus = await _leaveStatusRepository.GetLeaveStatusByIdAsync(leaveStatusId);
        //    if (leaveStatus == null)
        //    {
        //        return null;
        //    }
        //    if(leaveStatus.leaveStatusNameCode == "APV")
        //    {

        //    }



        //    return updateLeave;
           

        //}
        
        //public async Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync(long employeeId)
        //{
        //    try
        //    {
        //        Expression<Func<AppliedLeave, bool>> filter = la => la.employeeId == employeeId;
        //        IReadOnlyCollection<AppliedLeave> leaveAllocations = await _leaveRepository.GetAppliedLeavesAsync(filter);

        //        return leaveAllocations;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception here, you can log it or take appropriate action
        //        // You can also throw a custom exception or return a default value if needed.
        //        //Console.WriteLine("An error occurred: " + ex.Message);
        //        throw ex; // rethrow the exception or return a default value as needed
        //    }
        //}


        public async Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id)
        {
            var singleLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);
            
            //updateLeaveStatus
            return singleLeave;
        }   

        public async Task<AppliedLeave> UpdateAppliedLeaveAsync(long id, AppliedLeave leave)
        {

            var updateLeave = await _leaveRepository.UpdateAppliedLeaveAsync(id, leave);
            return updateLeave;
          
        }

        
        public async Task DeleteAppliedLeaveByIdAsync(long id)
        {
              await _leaveRepository.DeleteAppliedLeaveByIdAsync(id);
            
            
        }

        public async Task<AppliedLeave> CancelAppliedLeaveByIdAsync(long id)
        {
            
            var cancelledLeave = await _leaveRepository.CancelAppliedLeaveByIdAsync(id);
            //var employeeLeave = await _employeeLeaveRepository.GetEmployeeLeaveByEmployee(cancelledLeave.employeeId,cancelledLeave.leaveTypeId);
            //employeeLeave.balanceLeaves = employeeLeave.balanceLeaves + cancelledLeave.applyLeaveDay;
            //employeeLeave.consumedLeaves = employeeLeave.consumedLeaves - cancelledLeave.applyLeaveDay;
            //var cancelLeaveUpdate = await _leaveRepository.UpdateAppliedLeaveAsync(cancelledLeave);
            return cancelledLeave;
        }


        public async Task<AppliedLeave> UpdateIsRejectedAsync(long id, bool isRejected)
        {
            var applyLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);
          
            applyLeave.RejectedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            applyLeave.IsRejected = true;
            applyLeave.RejectedNotes = "";


            var applyLeaveUpdate = await _leaveRepository.UpdateAppliedLeaveAsync(applyLeave);

            return applyLeaveUpdate;
        }

        public async Task<AppliedLeave> UpdateIsApprovedAsync(long id, bool isApproved)
        {


            var applyLeave =   await _leaveRepository.GetAppliedLeaveByIdAsync(id);

            var employeeLeave  = await _employeeLeaveRepository.GetEmployeeLeaveByEmployee(applyLeave.employeeId, applyLeave.leaveTypeId);
            //adding here
           
            
                employeeLeave.balanceLeaves = employeeLeave.balanceLeaves - applyLeave.applyLeaveDay; //ERROR CHE
                employeeLeave.consumedLeaves = employeeLeave.consumedLeaves + applyLeave.applyLeaveDay;
     

            var employeeLeaveUpdate = await _employeeLeaveRepository.UpdateEmployeeLeaveAsync(employeeLeave.employeeLeaveId, employeeLeave);

            //applyLeave.ApprovedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
           // applyLeave.ApprovedDate = DateTime.Now;

            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            applyLeave.ApprovedDate = utcDateTime;
            applyLeave.IsApproved = true;
            applyLeave.ApprovedNotes = "";

            var applyLeaveUpdate  =  await _leaveRepository.UpdateAppliedLeaveAsync(applyLeave);


            return applyLeaveUpdate;
        }
        public async Task<AppliedLeave> UpdateIsApprovedCancelAsync(long id, bool isApproved)
        {


            var applyLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);

            var employeeLeave = await _employeeLeaveRepository.GetEmployeeLeaveByEmployee(applyLeave.employeeId, applyLeave.leaveTypeId);
            employeeLeave.balanceLeaves = employeeLeave.balanceLeaves + applyLeave.applyLeaveDay;
            employeeLeave.consumedLeaves = employeeLeave.consumedLeaves - applyLeave.applyLeaveDay;

            var employeeLeaveUpdate = await _employeeLeaveRepository.UpdateEmployeeLeaveAsync(employeeLeave.employeeLeaveId, employeeLeave);

            applyLeave.RejectedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            applyLeave.IsRejected = true;
            applyLeave.RejectedNotes = "";

            var applyLeaveUpdate = await _leaveRepository.UpdateAppliedLeaveAsync(applyLeave);


            return applyLeaveUpdate;
        }
        //  public async Task<IReadOnlyCollection<AppliedLeave>> GetPreviousAppliedLeavesAsync(
        //long employeeId = 0,
        //int leaveTypeId = 0,
        //bool isApproved = false,
        //bool isHalfDay = false,
        //Expression<Func<AppliedLeave, object>> orderBy = null)
        //  {
        //      // Create a parameter expression for the AppliedLeave type
        //      var parameter = Expression.Parameter(typeof(AppliedLeave));

        //      // Create a list of filter conditions based on the provided parameters
        //      var conditions = new List<Expression<Func<AppliedLeave, bool>>>();

        //      conditions.Add(c => c.employeeId == employeeId);
        //      conditions.Add(c => c.leaveTypeId == leaveTypeId);
        //      conditions.Add(c => c.IsApproved == isApproved);
        //      conditions.Add(c => c.IsHalfDay == isHalfDay);

        //      // Start with a condition that always evaluates to true
        //      var combinedCondition = Expression.Constant(true);

        //      // Combine all conditions with logical AND
        //      foreach (var condition in conditions)
        //      {
        //          combinedCondition = Expression.AndAlso(combinedCondition, Expression.Invoke(condition, parameter));
        //      }

        //      // Create the final filter expression
        //      var filter = Expression.Lambda<Func<AppliedLeave, bool>>(combinedCondition, parameter);

        //      // Pass the filter expression to the repository method
        //      return await _leaveRepository.GetFilteredLeavesAsync(filter, orderBy);
        //  }



        public async Task<IReadOnlyCollection<AppliedLeave>> GetPreviousAppliedLeavesAsync2(

       long employeeId = 0,
       int leaveTypeId = 0, bool isApproved = false, bool isHalfDay = false, Expression<Func<AppliedLeave, object>> orderBy = null)
        {
            var parameter = Expression.Parameter(typeof(AppliedLeave));

            // Create a list of conditions to apply
            var conditions = new List<Expression<Func<AppliedLeave, bool>>>();
            conditions.Add(c => c.employeeId == employeeId);
            conditions.Add(c => c.leaveTypeId == leaveTypeId);
            conditions.Add(c => c.IsApproved == isApproved);
            conditions.Add(c => c.IsHalfDay == isHalfDay);


            // Combine all conditions with logical AND
            var combinedCondition = conditions
                .Aggregate((current, next) =>
                    Expression.Lambda<Func<AppliedLeave, bool>>(
                        Expression.AndAlso(current.Body, next.Body),
                        parameter
                    )
                );

            // Apply the combined condition to the filter
            var filter = Expression.Lambda<Func<AppliedLeave, bool>>(
                combinedCondition.Body,
                parameter
            );

            return await _leaveRepository.GetFilteredLeavesAsync(filter, orderBy);
        }

        public async Task<IReadOnlyCollection<AppliedLeave>> GetUnApprovedAppliedLeavesAsync(AppliedLeave appliedLeave)
        {
          var unApprovedAppliedLeaves =  await _leaveRepository.GetUnApprovedAppliedLeavesAsync(appliedLeave);

            return unApprovedAppliedLeaves;
        }

        //public async Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter)
        //{
        //    return await _leaveRepository.GetAppliedLeavesAsync(filter);
        //}

        public async Task<IEnumerable<AppliedLeaveDTO>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter)
        {
            var appliedLeaves = await _leaveRepository.GetAppliedLeavesAsync(filter);

            var appliedLeaveDTOs = appliedLeaves.Select(appliedLeave => new AppliedLeaveDTO
            {
                appliedLeaveTypeId = appliedLeave.appliedLeaveTypeId,
                employeeId = appliedLeave.employeeId,
                FirstName = appliedLeave.Employee?.firstName ?? string.Empty,
                LastName = appliedLeave.Employee?.lastName ?? string.Empty,
                StartDate = appliedLeave.StartDate,
                EndDate = appliedLeave.EndDate,
                LeaveTypeName = appliedLeave.LeaveType?.leaveTypeName ?? string.Empty,
                BalanceLeave = appliedLeave.balanceLeave, // Convert double to int if necessary
                AppliedLeave = appliedLeave.applyLeaveDay, // Convert double to int if necessary
                RemaingLeave = appliedLeave.remaingLeave, // Convert double to int if necessary
                LeaveReason = appliedLeave.LeaveReason ?? string.Empty,
                ApplyLeaveDay = appliedLeave.applyLeaveDay,
                isApproved = appliedLeave.IsApproved,
                isRejected = appliedLeave.IsRejected,
                LeaveStatusCode = appliedLeave.LeaveStatus?.LeaveStatusCode ?? string.Empty,
                LeaveStatusName = appliedLeave.LeaveStatus?.LeaveStatusName ?? string.Empty,
            }).ToList();

            return appliedLeaveDTOs;
        }

        public async Task<AppliedLeave> AppliedLeaveUpdateStatusAsync(AppliedLeaveUpdateStatus appliedLeaveUpdateStatus)
        {
            try
            {
              
                var existingLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(appliedLeaveUpdateStatus.appliedLeaveTypeId);
            
                if (existingLeave == null)
                {
                  
                    throw new ArgumentNullException(nameof(existingLeave), "Leave not found");
                }
            
                var leaveStatus = await _leaveStatusService.GetLeaveStatusByCodeAsync(appliedLeaveUpdateStatus.statusCode);
           
                if (leaveStatus == null)
                {
                  
                    throw new ArgumentNullException(nameof(leaveStatus), "Leave status not found. " + appliedLeaveUpdateStatus.statusCode);
                }


                if (existingLeave.LeaveStatus.LeaveStatusCode == appliedLeaveUpdateStatus.statusCode)
                {
                 

                    throw new CustomLeaveException("The leave is already "+ leaveStatus.LeaveStatusName,900);
                }




                bool allowNotToTrack = true; // Variable indicating whether tracking is allowed


              
                Expression<Func<EmployeeLeave, bool>> filter = x =>
                  x.employeeId == existingLeave.employeeId &&
                  x.leaveTypeId == existingLeave.leaveTypeId &&
                  x.leaveAllocationId == appliedLeaveUpdateStatus.leaveAllocationId;
                 
                  var  employeeLeave = await _employeeLeaveRepository.GetEmployeeLeaveAsync(filter);
                        
              
               
                if (appliedLeaveUpdateStatus.statusCode == "APR")
                {
                   
                    employeeLeave.balanceLeaves -= existingLeave.applyLeaveDay;
                    employeeLeave.consumedLeaves += existingLeave.applyLeaveDay;
                  
                }

                if (appliedLeaveUpdateStatus.statusCode == "APC")
                {
                 
                    employeeLeave.balanceLeaves += existingLeave.applyLeaveDay;
                    employeeLeave.consumedLeaves -= existingLeave.applyLeaveDay;
                 
                }

                if (appliedLeaveUpdateStatus.statusCode == "APR" || appliedLeaveUpdateStatus.statusCode == "APC")
                {
                
                    var UpdateemployeeLeave = await _employeeLeaveRepository.UpdateEmployeeLeaveAsync(employeeLeave);
                  
                }

                existingLeave.LeaveStatusId = leaveStatus.LeaveStatusId;
                existingLeave.LeaveStatus = leaveStatus;


                try
                {
                    
                  var  applyLeaveUpdate = await _leaveRepository.UpdateAppliedLeaveAsync(existingLeave);
                    

                    return applyLeaveUpdate;
                }
                catch (Exception ex)
                {
                  
                    throw;
                }

              
            }
            catch (Exception ex)
            {
                await _emailService.SendErrorMail("ved.thakur@wonderbiz.in", ex.Message, "AppliedLeaveUpdateStatusAsync");
                throw;
            }
        }
    }
}
