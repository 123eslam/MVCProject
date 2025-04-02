using Demo.DAL.Entities.Identity;
using System.Net;
using System.Net.Mail;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var clint = new SmtpClient("smtp.gmail.com", 587);
            clint.EnableSsl = true;
            //Sender Email
            clint.Credentials = new NetworkCredential("eslameltamalawy124@gmail.com", "hcfltldgsrgpodhr");//Generate app password
            //Receiver Email
            clint.Send("eslameltamalawy124@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
