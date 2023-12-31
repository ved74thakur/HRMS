﻿using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;
        public HolidayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Holiday>> GetHolidaysAsync()
        {
            return await _context.Holidays.OrderBy(h => h.HolidayDate).ToListAsync();
        }

        public async Task<Holiday> GetHolidayByIdAsync(int id)
        {
            var singleHoliday = await _context.Holidays.FindAsync(id);
            if (singleHoliday == null)
            {
                return null;
            }
            return singleHoliday;

        }

        public async Task<Holiday> CreateHoliday(Holiday holiday)
        {
            if (_context.Holidays.Any(h => h.HolidayDate == holiday.HolidayDate))
            {
                throw new ArgumentException("A holiday with same date already exists");
            }
            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();
            return holiday;

        }

        public async Task<Holiday> UpdateHoliday(Holiday holiday)
        {
            _context.Entry(holiday).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return holiday;
        }
        public async Task<Holiday> DeleteHoliday(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null)
            {
                return null;

            }
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return holiday;
        }
    }
}
