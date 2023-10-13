using System.Net;
using System.Net.Mail;

namespace Leave.EmailTemplate
{
    public class GenericEmail
    {

        public async Task SendEmailAsync(string toEmail, string subject, string body, long appliedLeaveTypeId)
        {
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            body += $"<p>Email sent on: {formattedDateTime}</p>";

            body += "<p>Please click one of the following buttons:</p>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";


            
            var mailMessage = new MailMessage
            {
                From = new MailAddress("ved74thakur@gmail.com", "Ved Thakur"),
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
                    Credentials = new NetworkCredential("ved74thakur@gmail.com", "eyom ydgc hfae pqvi"),
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
            body += "<p>Please click one of the following buttons:</p>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("ved74thakur@gmail.com", "Ved Thakur"),
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
                    Credentials = new NetworkCredential("ved74thakur@gmail.com", "eyom ydgc hfae pqvi"),
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
        public async Task SendEmailAsync(string email, string subject, string body, string emailCC, string emailBCC, long appliedLeaveTypeId)
        {
            body += "<p>Please click one of the following buttons:</p>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("ved74thakur@gmail.com", "Ved Thakur"),
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
                    Credentials = new NetworkCredential("ved74thakur@gmail.com", "eyom ydgc hfae pqvi"),
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
            body += "<p>Please click one of the following buttons:</p>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";
            var mailMessage = new MailMessage
            {
                From = new MailAddress("ved74thakur@gmail.com", "Ved Thakur"),
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
                    Credentials = new NetworkCredential("ved74thakur@gmail.com", "eyom ydgc hfae pqvi"),
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
    }
}









