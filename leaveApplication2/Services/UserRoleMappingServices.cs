using leaveApplication2.Dtos;
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

        public async Task<List<UserRoleMappingDTO>> CreateUserRoleMappings(List<UserRoleMappingDTO> mappings)
        {
            try
            {
                // You may want to perform validation or other operations here before creating the mappings.

                // Pass the list of mappings to the repository to create them.
                List<UserRoleMappingDTO> createdMappings = await _mappingRepository.CreateUserRoleMappings(mappings);

                return createdMappings;
            }
            catch (Exception ex)
            {
                // Handle the exception here, log it, or take appropriate action.
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }
        /* public async Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping mapping)
        {
            // Other code here...

            UserRoleMapping createdMapping = null; // Declare it here

            try
            {
                createdMapping = await _mappingRepository.CreateUserRoleMappings(mapping);
                // Other code...
            }
            catch (Exception ex)
            {
                // Handle exceptions...
                throw;
            }

            // Return 'createdMapping' here
            return createdMapping;
        } */

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
