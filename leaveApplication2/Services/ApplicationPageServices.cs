using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class ApplicationPageServices : IApplicationPageServices
    {
        private readonly IApplicationPageRepository _pageRepository;

        public ApplicationPageServices(IApplicationPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<IEnumerable<ApplicationPage>> GetApplicationPagesAsync()
        {
            return await _pageRepository.GetApplicationPagesAsync();
        }

        public async Task<ApplicationPage> CreateApplicationPageAsync(ApplicationPage page)
        {
            return await _pageRepository.CreateApplicationPage(page);
        }

        public async Task<ApplicationPage> GetApplicationPageByIdAsync(int id)
        {
            return _pageRepository.GetById(id);
        }

        public async Task<ApplicationPage> UpdateApplicationPageAsync(int id, ApplicationPage page)
        {
            var existingPage = _pageRepository.GetById(id);
            if (existingPage == null)
            {
                // Handle the case where the page doesn't exist
                return null;
            }

            // Update the properties of the existing page with the values from the provided 'page' object
            existingPage.PageName = page.PageName; // Replace 'Property1' with the actual property name
            existingPage.PageCode = page.PageCode;
            existingPage.IsActive = page.IsActive;// Replace 'Property2' with the actual property name

            return await _pageRepository.UpdateApplicationPage(existingPage);
        }

        public async Task DeleteApplicationPageAsync(int id)
        {
            await _pageRepository.DeleteApplicationPageAsync(id);
        }
    }
}
