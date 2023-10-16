using Leave.EmailTemplate;
using leaveApplication2.Data;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class PasswordResetService : IPasswordResetService
    {
       
        private readonly GenericEmail _genericEmail;
        
        private readonly IEmployeeRepository _employeeRepository;
        

        public PasswordResetService( GenericEmail genericEmail, IEmployeeRepository employeeRepository)
        {
           
            
            _genericEmail = genericEmail;
            _employeeRepository = employeeRepository;
        }
        

        public async void SendPasswordResetEmail(string email)
        {
            // Implement logic to send a password reset email using the EmailService
            // You might generate a unique token for the password reset link and include it in the email.
            // Send the email to the user associated with the provided email address.
            // Example: _emailService.SendPasswordResetEmail(email, resetToken);
            var resetToken = "hghghgh";
            var employeeFetch = await _employeeRepository.GetEmployeeByEmailAsync(email);


            _genericEmail.SendPasswordResetEmailGeneric(email, resetToken,employeeFetch.employeeId);
        }
    }
}
