using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;
        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }
        public async Task<IReadOnlyCollection<Designation>> GetDesignationsAsync()
        {

            try
            {
                var designations = await _designationRepository.GetDesignationsAsync();
                return designations;
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
