using System.Net;
using System.Net.Mail;

namespace DentalProject_Graduation.Data.Helper
{
    public class SenderMail
    {
        public static string SendMailA(string title, string message ,string MailRevicer)
        {
            try
            {
                //connect to server google
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //make encrpt message
                smtp.EnableSsl = true;
                //make auth
                smtp.Credentials = new NetworkCredential("alameer.samy99@gmail.com", "iqgkxwbtlhfacosw");
                // send mail to  form 
                smtp.Send("alameer.samy99@gmail.com", MailRevicer, title, message);

                return "good";
            }
            catch
            {
                return "bad";
            }



        }
    }
}
