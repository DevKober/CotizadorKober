using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorKober.Models
{
    public static class Mailing
    {
        public static bool SendMail(string emailTo, string subject, string bodyMsg)
        {
            try
            {
                MailMessage mail = new MailMessage("info@kober.com.mx", emailTo);
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info@kober.com.mx", "K0ber.2014");
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                mail.Subject = subject;
                mail.Body = bodyMsg;
                mail.IsBodyHtml = true;
                client.Send(mail);
                return true;
            }
            catch { return false; }
        }
    }
}
