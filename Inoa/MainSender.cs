using System.Net;
using System.Net.Mail;

namespace Inoa;

class MailSender
{
    public static void SendMail(string subject, string body)
    {
        string username = EnvReader.GetVariable("SMTP_USER");
        string password = EnvReader.GetVariable("SMTP_PASS");
        string emailTo  = EnvReader.GetVariable("MAIL_TO");

        SmtpClient client = new()
        {
            Host                  = EnvReader.GetVariable("SMTP_SERVER"),
            Port                  = int.Parse(EnvReader.GetVariable("SMTP_PORT")),
            EnableSsl             = true,
            Credentials           = new NetworkCredential(username, password),
            DeliveryMethod        = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
        };


        MailMessage message = new()
        {
            From    = new MailAddress(username),
            Subject = subject,
            Body    = body
        };

        message.To.Add(new MailAddress(emailTo));

        client.Send(message);
    }
}
