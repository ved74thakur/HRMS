using System.Net;
using System.Net.Mail;

namespace Leave.EmailTemplate
{
    public class GenericEmail
    {

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
           
          
            
            var mailMessage = new MailMessage
            {
                From = new MailAddress("appdev.application23@gmail.com", "HRMS"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);
           
            //mailMessage.To.Add("ved.thakur@wonderbiz.in");
            try
            {

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Port for TLS/STARTTLS
                    Credentials = new NetworkCredential("appdev.application23@gmail.com", "tphn ljsc jwng eyjo"),
                    EnableSsl = true // Enable SSL/TLS encryption
                };
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Add error handling and logging here.
                throw ex;
            }
        }

        public async Task SendEmailAsync(List<string> emails, string subject, string body, string emailCC , string emailBCC, long appliedLeaveTypeId)
        {
           
            var mailMessage = new MailMessage
            {
                From = new MailAddress("appdev.application23@gmail.com", "HRMS"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            foreach (string email in emails)
            {
                mailMessage.To.Add(email);
            }

            if (!string.IsNullOrEmpty(emailCC))
            {
                mailMessage.CC.Add(emailCC);
            }

            if (!string.IsNullOrEmpty(emailBCC))
            {
                mailMessage.Bcc.Add(emailBCC);
            }

            try
            {

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Port for TLS/STARTTLS
                    Credentials = new NetworkCredential("appdev.application23@gmail.com", "tphn ljsc jwng eyjo"),
                    EnableSsl = true // Enable SSL/TLS encryption
                };
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions here (e.g., log the error or take appropriate action).
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
        public async Task SendEmailAsync(string email, string subject, string body, string emailCC, string emailBCC)
        {
           
            var mailMessage = new MailMessage
            {
                From = new MailAddress("appdev.application23@gmail.com", "HRMS"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            
             mailMessage.To.Add(email);
            
            

            if (!string.IsNullOrEmpty(emailCC))
            {
                mailMessage.CC.Add(emailCC);
            }

            if (!string.IsNullOrEmpty(emailBCC))
            {
                mailMessage.Bcc.Add(emailBCC);
            }
         

            try
            {

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Port for TLS/STARTTLS
                    Credentials = new NetworkCredential("appdev.application23@gmail.com", "tphn ljsc jwng eyjo"),
                    EnableSsl = true // Enable SSL/TLS encryption
                };
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Add error handling and logging here.
                throw ex;
            }
        }

        public async Task SendEmail(List<string> emails, string subject, string body, long appliedLeaveTypeId)
        {
           
            var mailMessage = new MailMessage
            {
                From = new MailAddress("appdev.application23@gmail.com", "HRMS"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            foreach (string email in emails)
            {
                mailMessage.To.Add(email);
            }
            try
            {

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Port for TLS/STARTTLS
                    Credentials = new NetworkCredential("appdev.application23@gmail.com", "tphn ljsc jwng eyjo"),
                    EnableSsl = true // Enable SSL/TLS encryption
                };
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Add error handling and logging here.
                throw ex;
            }
        }

        public void SendPasswordResetEmailGeneric(string recipientEmail, string resetToken, long employeeId)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("appdev.application23@gmail.com", "tphn ljsc jwng eyjo"),
                    EnableSsl = true // Enable SSL/TLS encryption
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("appdev.application23@gmail.com", "HRMS"),
                    Subject = "Password Reset",
                    Body = $"<p>Click the link below to reset your password:</p>" +
                           $"<p><a href='{GeneratePasswordResetLink(resetToken, employeeId)}'>Reset Password</a></p>",
                    IsBodyHtml = true,
                };

                mail.To.Add(new MailAddress(recipientEmail));

                client.Send(mail);
            }
            catch (Exception ex)
            {
                // Handle email sending failure here
                throw new Exception("Email sending failed: " + ex.Message);
            }

        }
        private string GeneratePasswordResetLink(string resetToken, long employeeId)
        {
            //amit se link
            // Generate the URL for password reset, including the resetToken
            // Example: return $"https://yourwebsite.com/reset-password?token={resetToken}";
            //return $"http://localhost:3000/updatepassword/2?token={resestToken}/{employeeId}"
            return $"http://192.168.1.37:85/updatepassword/{employeeId}?token={resetToken}/";
        }
    }
}









