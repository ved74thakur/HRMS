using Leave.EmailTemplate;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAssignController : ControllerBase
    {
        private readonly IRoleAssignService _roleAssignService;
        private readonly ILogger<RoleAssignController> _logger;

        public RoleAssignController(IRoleAssignService roleAssignService, ILogger<RoleAssignController> logger)
        {
            _roleAssignService = roleAssignService;
            _logger = logger;
        }

        [HttpGet("GetRoleAssignsAsync")]
        public async Task<CommonResponse<IEnumerable<RoleAssign>>> GetRoleAssignsAsync()
        {
            _logger.LogInformation("Start GetRoleAssignsAsync");
            try
            {
                var roleAssigns = await _roleAssignService.GetRoleAssignsAsync();

                if (roleAssigns == null)
                {
                    _logger.LogInformation("Start GetRoleAssignsAsync null");
                    return this.CreateResponse <IEnumerable<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No RoleAssigns found.");
                }

                _logger.LogInformation("End GetRoleAssignsAsync");
                return this.CreateResponse <IEnumerable<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", roleAssigns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving RoleAssigns.");
                return this.CreateResponse <IEnumerable<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //Create leaves
        [HttpPost("CreateRoleAssigneAsync")]
        public async Task<CommonResponse<ActionResult<RoleAssign>>> CreateRoleAssigneAsync(RoleAssign role)
        {
            _logger.LogInformation("Start CreateRoleAssigneAsync");

            try
            {

                var newRoleAssign = await _roleAssignService.CreateRoleAssign(role);

                if (newRoleAssign == null)
                {
                    _logger.LogInformation("Start AddRoleAssign null");
                    return this.CreateResponse<ActionResult<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutations found.");
                }

                _logger.LogInformation("Get the values of newRoleAssign");
                _logger.LogInformation("End CreateRoleAssigneAsync");


                return this.CreateResponse<ActionResult<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Role Created Successfully", newRoleAssign);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new leave request");
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return this.CreateResponse<ActionResult<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message + ex.InnerException);
            }
        }
        [HttpPost("UpdateRoleAssignAsync")]
        public async Task<CommonResponse<ActionResult<RoleAssign>>> UpdateRoleAssignAsync([FromBody] RoleAssign role)
        {
            _logger.LogInformation($"Start UpdateEmployeeByIdAsync");
            try
            {

                var updatedRole = await _roleAssignService.UpdateRoleAssign(role);

                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedRole);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return this.CreateResponse<ActionResult<RoleAssign>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [HttpDelete("DeleteRoleAssignAsync/{id}")]
        public async Task<CommonResponse<ActionResult>> DeleteRoleAssignAsync(int id)
        {
            _logger.LogInformation($"Start DeleteRoleAssignAsync");

            try
            {
                // Call the service to delete the RoleAssign by Id
                await _roleAssignService.DeleteRoleAssignAsync(id);

                _logger.LogInformation($"RoleAssign deleted successfully");

                return this.CreateResponse<ActionResult>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "RoleAssign deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the RoleAssign");
                return this.CreateResponse<ActionResult>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
