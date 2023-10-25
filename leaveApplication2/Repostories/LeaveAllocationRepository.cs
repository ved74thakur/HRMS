﻿using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public class LeaveAllocationRepository: ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public LeaveAllocationRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync()
        {
            return await _context.LeaveAllocations.AsNoTracking().ToListAsync();
        }
        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync(Expression<Func<LeaveAllocation, bool>> filter)
        {
            

            return await _context.LeaveAllocations.Where(filter).ToListAsync();



        }
        public async Task<LeaveAllocation> GetLeaveAllocationAsync(long id)
        {
            var leaveAllocation = await _context.LeaveAllocations.FindAsync(id);
            if (leaveAllocation == null)
            {
                return null;
            }
            return leaveAllocation;
        }
        public async Task<LeaveAllocation> GetLeaveAllocationAsync(Expression<Func<LeaveAllocation, bool>> filter)
        {
            var leaveAllocation = await _context.LeaveAllocations.FirstOrDefaultAsync(filter);

            if (leaveAllocation == null)
            {
                return null;
            }

            return leaveAllocation;
        }

    }
}