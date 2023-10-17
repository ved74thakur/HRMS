using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IHolidayRepository
    {
        Task<IReadOnlyCollection<Holiday>> GetHolidaysAsync();
        Task<Holiday> GetHolidayByIdAsync(int id);
        Task<Holiday> CreateHoliday(Holiday holiday);

        Task<Holiday> UpdateHoliday(Holiday holiday);
        Task<Holiday> DeleteHoliday(int id);
    }
}
