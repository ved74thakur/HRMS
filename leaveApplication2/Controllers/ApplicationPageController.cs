    using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationPageController : ControllerBase
    {
        private readonly IApplicationPageServices _pageService;
        private readonly ILogger<ApplicationPageController> _logger;

        public ApplicationPageController(IApplicationPageServices pageService, ILogger<ApplicationPageController> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }
        //changes done

        // Get all application pages
        [HttpGet("GetApplicationPagesAsync")]
        public async Task<CommonResponse<IEnumerable<ApplicationPages>>> GetApplicationPagesAsync()
        {
            _logger.LogInformation("Start GetApplicationPagesAsync");
            try
            {
                var pages = await _pageService.GetApplicationPagesAsync();

                if (pages == null)
                {
                    return this.CreateResponse<IEnumerable<ApplicationPages>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No application pages found.");
                }

                _logger.LogInformation("End GetApplicationPagesAsync");
                return this.CreateResponse<IEnumerable<ApplicationPages>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", pages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving application pages.");
                return this.CreateResponse<IEnumerable<ApplicationPages>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Get a single application page by ID
        [HttpGet("GetApplicationPageByIdAsync/{id}")]
        public async Task<CommonResponse<ApplicationPages>> GetApplicationPageByIdAsync(int id)
        {
            _logger.LogInformation("Start GetApplicationPageByIdAsync");
            try
            {
                var page = await _pageService.GetApplicationPageByIdAsync(id);

                if (page == null)
                {
                    return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Application page not found.");
                }

                _logger.LogInformation("End GetApplicationPageByIdAsync");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", page);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the application page.");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Create a new application page
        [HttpPost("CreateApplicationPageAsync")]
        public async Task<CommonResponse<ApplicationPages>> CreateApplicationPageAsync(ApplicationPages page)
        {
            _logger.LogInformation("Start CreateApplicationPageAsync");

            try
            {
                var newPage = await _pageService.CreateApplicationPageAsync(page);

                if (newPage == null)
                {
                    return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Failed to create the application page.");
                }

                _logger.LogInformation("End CreateApplicationPageAsync");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Application page created successfully", newPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new application page.");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update an application page
        [HttpPost("UpdateApplicationPageAsync/{id}")]
        public async Task<CommonResponse<ApplicationPages>> UpdateApplicationPageAsync(int id, ApplicationPages page)
        {
            _logger.LogInformation("Start UpdateApplicationPageAsync");

            try
            {
                var updatedPage = await _pageService.UpdateApplicationPageAsync(id, page);

                if (updatedPage == null)
                {
                    return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Failed to update the application page.");
                }

                _logger.LogInformation("End UpdateApplicationPageAsync");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Application page updated successfully", updatedPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the application page.");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Delete an application page by ID
        [HttpDelete("DeleteApplicationPageAsync/{id}")]
        public async Task<CommonResponse<ApplicationPages>> DeleteApplicationPageAsync(int id)
        {
            _logger.LogInformation("Start DeleteApplicationPageAsync");

            try
            {
                await _pageService.DeleteApplicationPageAsync(id);

                _logger.LogInformation("End DeleteApplicationPageAsync");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Application page deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the application page.");
                return this.CreateResponse<ApplicationPages>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
