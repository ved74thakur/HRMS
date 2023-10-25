using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayService(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }
        public async Task<IEnumerable<Holiday>> GetHolidaysAsync()
        {
            return await _holidayRepository.GetHolidaysAsync();

        }

        public async Task<Holiday> GetHolidayByIdAsync(int id)
        {
            var singleHoliday = await _holidayRepository.GetHolidayByIdAsync(id);
            //updateLeaveStatus
            return singleHoliday;
        }

        public async Task<Holiday> CreateHoliday(Holiday holiday)
        {
            //var singleLeave = await _leaveRepository.GetAppliedLeaveByIdAsync(id);
            //leave.leaveStatusId = singleLeave.asdsda;

            var createdHoliday = await _holidayRepository.CreateHoliday(holiday);
            return createdHoliday;
        }

        public async Task<Holiday> UpdateHoliday(Holiday holiday)
        {
            var selectedHoliday = await _holidayRepository.GetHolidayByIdAsync(holiday.Id);
            if (selectedHoliday == null)
            {
                return null;
            }
            var updatedHoliday = await _holidayRepository.UpdateHoliday(selectedHoliday);
            return updatedHoliday;
        }

        public async Task<Holiday> DeleteHoliday(int id)
        {
            var deletedHoliday = await _holidayRepository.DeleteHoliday(id);
            if (deletedHoliday == null)
            {
                return null;
            }
            return deletedHoliday;
        }
    }
}
