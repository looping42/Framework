using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mail
{
    public class Mailer
    {
        public static void SendMail(string from, string to, string cc, string subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to, subject, Body);
                SmtpClient client = new SmtpClient(Properties.Settings.Default.FMMailer, 587);
                client.EnableSsl = true;
                //client.Credentials = new NetworkCredential("","" );
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}