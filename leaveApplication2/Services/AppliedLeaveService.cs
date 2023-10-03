using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace leaveApplication2.Services
{
    public class AppliedLeaveService : IAppliedLeaveService
    {
        private readonly IAppliedLeaveRepository _leaveRepository;

        public AppliedLeaveService(IAppliedLeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync()
        {
            return await _leaveRepository.GetAppliedLeavesAsync();
        }

        public async Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave)
        {
            //var singleLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);
            //leave.leaveStatusId = singleLeave.asdsda;

            var createdLeave = await _leaveRepository.CreateAppliedLeave(leave);
            return createdLeave;
        }

        //updateLeaveStatus(appliedLeaveTypeId, statusIdKey){
        //var singleLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);
        //changeupdated
        //var result await _leaveRepository.UpdateAppliedLeaveAsync(id, leave);
        //Return result


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
        

        
    }
}
