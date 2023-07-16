using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace DentalProject_Graduation.Data.Helper
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var FormMail = "alameer.samy99@gmail.com";
            var FormPassword = "iqgkxwbtlhfacosw";
            var message = new MailMessage();
            message.From = new MailAddress(FormMail);
            message.Subject = subject;  
            message.Body = $"<html><boby>{htmlMessage}</html></boby>";
            message.IsBodyHtml = true;  
            message.To.Add(email);

            //
            //connect to server google
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //make encrpt message
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(FormMail, FormPassword);
            smtp.Send(message);

        }
    }
}
