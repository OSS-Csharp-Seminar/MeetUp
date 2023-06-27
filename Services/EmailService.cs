using MeetUp.Data;
using MeetUp.Interfaces;
using System.Net.Mail;
using System.Net;


namespace MeetUp.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(EmailMessage emailMessage)
        {
            string senderEmail = Environment.GetEnvironmentVariable("EMAIL") ?? "";
            string password = Environment.GetEnvironmentVariable("PASSWORD") ?? "";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: senderEmail,
                                to: emailMessage.Email,
                                emailMessage.Subject,
                                emailMessage.Message
                                ));
        }
    }
}
