using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace leaveApplication2.Services
{
    public class UserRoleMappingServices : IUserRoleMappingServices
    {
        private readonly IUserRoleMappingRepository _mappingRepository;

        public UserRoleMappingServices(IUserRoleMappingRepository mappingRepository)
        {
            _mappingRepository = mappingRepository;
        }

        public async Task<IEnumerable<UserRoleMapping>> GetUserRoleMappingsAsync()
        {
            return await _mappingRepository.GetUserRoleMappingsAsync();
        }

        public async Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping mapping)
        {
            // Other code here...

            UserRoleMapping createdMapping = null; // Declare it here

            try
            {
                createdMapping = await _mappingRepository.CreateUserRoleMapping(mapping);
                // Other code...
            }
            catch (Exception ex)
            {
                // Handle exceptions...
                throw;
            }

            // Return 'createdMapping' here
            return createdMapping;
        }

        public async Task<UserRoleMapping> GetUserRoleMappingByIdAsync(int id)
        {
            return await _mappingRepository.GetUserRoleMappingById(id);
        }

        public async Task<UserRoleMapping> UpdateUserRoleMappingAsync(int id, UserRoleMapping mapping)
        {
            var existingMapping = await _mappingRepository.GetUserRoleMappingById(id); // Add await here
            if (existingMapping == null)
            {
                return null;
            }

            // Update the properties of the existing mapping with the values from the provided 'mapping' object
            // Example: existingMapping.Property = mapping.Property;

            return await _mappingRepository.UpdateUserRoleMapping(existingMapping);
        }

        public async Task DeleteUserRoleMappingAsync(int id)
        {
            await _mappingRepository.DeleteUserRoleMappingById(id);
        }
    }
}
