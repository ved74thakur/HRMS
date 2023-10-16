using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _GenderRepository;
        public GenderService(IGenderRepository GenderRepository)
        {
            _GenderRepository = GenderRepository;
        }
        public async Task<IReadOnlyCollection<Gender>> GetGendersAsync()
        {

            try
            {
                var Genders = await _GenderRepository.GetGendersAsync();
                return Genders;
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log the error or throw a custom exception
                // You can also return an empty list or null if appropriate for your use case
                throw; // Re-throw the exception if you want to propagate it to the caller
            }
        }
    }
}
