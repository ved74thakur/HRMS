using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleMappingController : ControllerBase
    {
        private readonly IUserRoleMappingServices _mappingService;
        private readonly ILogger<UserRoleMappingController> _logger;

        public UserRoleMappingController(IUserRoleMappingServices mappingService, ILogger<UserRoleMappingController> logger)
        {
            _mappingService = mappingService;
            _logger = logger;
        }

        [HttpGet("GetUserRoleMappingsAsync")]
        public async Task<CommonResponse<IEnumerable<UserRoleMappingDTO>>> GetUserRoleMappingsAsync()
        {
            _logger.LogInformation("Start GetUserRoleMappingsAsync");
            try
            {
                var mappings = await _mappingService.GetUserRoleMappingsAsync();

                if (mappings == null)
                {
                    return this.CreateResponse<IEnumerable<UserRoleMappingDTO>>(StatusCodes.Status404NotFound, "No user-role mappings found.");
                }

                _logger.LogInformation("End GetUserRoleMappingsAsync");
                return this.CreateResponse<IEnumerable<UserRoleMappingDTO>>(StatusCodes.Status200OK, "Success", mappings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user-role mappings.");
                return this.CreateResponse<IEnumerable<UserRoleMappingDTO>>(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetUserRoleMappingByIdAsync/{id}")]
        public async Task<CommonResponse<UserRoleMapping>> GetUserRoleMappingByIdAsync(int id)
        {
            _logger.LogInformation("Start GetUserRoleMappingByIdAsync");
            try
            {
                var mapping = await _mappingService.GetUserRoleMappingByIdAsync(id);

                if (mapping == null)
                {
                    return this.CreateResponse<UserRoleMapping>(StatusCodes.Status404NotFound, "User-role mapping not found.");
                }

                _logger.LogInformation("End GetUserRoleMappingByIdAsync");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status200OK, "Success", mapping);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user-role mapping.");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateUserRoleMappingsAsync")]
        public async Task<CommonResponse<List<UserRoleMappingDTO>>> CreateUserRoleMappingsAsync(List<UserRoleMappingDTO> mappings)
        {
            _logger.LogInformation("Start CreateUserRoleMappingsAsync");

            try
            {
                var createdMappings = await _mappingService.CreateUserRoleMappings(mappings);

                if (createdMappings == null || createdMappings.Count == 0)
                {
                    return this.CreateResponse<List<UserRoleMappingDTO>>(StatusCodes.Status404NotFound, "Failed to create the user-role mappings.");
                }

                _logger.LogInformation("End CreateUserRoleMappingsAsync");
                return this.CreateResponse<List<UserRoleMappingDTO>>(StatusCodes.Status200OK, "User-role mappings created successfully", createdMappings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating new user-role mappings.");
                return this.CreateResponse<List<UserRoleMappingDTO>>(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /* [HttpPost("CreateUserRoleMappingAsync")]
         public async Task<CommonResponse<UserRoleMapping>> CreateUserRoleMappingAsync(UserRoleMapping mapping)
         {
             _logger.LogInformation("Start CreateUserRoleMappingAsync");

             try
             {
                 var newMapping = await _mappingService.CreateUserRoleMappingAsync(mapping);

                 if (newMapping == null)
                 {
                     return this.CreateResponse<UserRoleMapping>(StatusCodes.Status404NotFound, "Failed to create the user-role mapping.");
                 }

                 _logger.LogInformation("End CreateUserRoleMappingAsync");
                 return this.CreateResponse<UserRoleMapping>(StatusCodes.Status200OK, "User-role mapping created successfully", newMapping);
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "An error occurred while creating a new user-role mapping.");
                 return this.CreateResponse<UserRoleMapping>(StatusCodes.Status500InternalServerError, ex.Message);
             }
         } */

        [HttpPost("UpdateUserRoleMappingAsync/{id}")]
        public async Task<CommonResponse<UserRoleMapping>> UpdateUserRoleMappingAsync(int id, UserRoleMapping mapping)
        {
            _logger.LogInformation("Start UpdateUserRoleMappingAsync");

            try
            {
                var updatedMapping = await _mappingService.UpdateUserRoleMappingAsync(id, mapping);

                if (updatedMapping == null)
                {
                    return this.CreateResponse<UserRoleMapping>(StatusCodes.Status404NotFound, "Failed to update the user-role mapping.");
                }

                _logger.LogInformation("End UpdateUserRoleMappingAsync");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status200OK, "User-role mapping updated successfully", updatedMapping);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user-role mapping.");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteUserRoleMappingAsync/{id}")]
        public async Task<CommonResponse<UserRoleMapping>> DeleteUserRoleMappingAsync(int id)
        {
            _logger.LogInformation("Start DeleteUserRoleMappingAsync");

            try
            {
                await _mappingService.DeleteUserRoleMappingAsync(id);

                _logger.LogInformation("End DeleteUserRoleMappingAsync");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status200OK, "User-role mapping deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user-role mapping.");
                return this.CreateResponse<UserRoleMapping>(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
