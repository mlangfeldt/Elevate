using System.Net;
using System.Net.Mail;

namespace Elevate.BL
{

    // L: fvtcelevate@gmail.com
    // P: FVTCelevate2024
    // App Password: amwu gsoa mimh hoyn

    public class EmailService
    {
        public static void SendResetCodeEmail(string email, string code)
        {
            string body = $"Your password reset code is: {code}";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                // 7135
                Port = 587,
                Credentials = new NetworkCredential("fvtcelevate@gmail.com", "zwle jjjq ogmo tqkp"),
                EnableSsl = true,
            };

            smtpClient.Send("fvtcelevate@gmail.com", email, "Reset Password Code", body);

        }

        public static void SendConfirmationCodeEmail(string body, string email, string code)
        {

            //string body = "Welcome to Elevate!";
            //body += "<br /><br />Please click the following link to activate your account";
            //body += $"<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, code) + "'>Click here to activate your account.</a>";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                // 7135
                Port = 587,
                Credentials = new NetworkCredential("fvtcelevate@gmail.com", "zwle jjjq ogmo tqkp"),
                EnableSsl = true,

            };

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("fvtcelevate@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "Welcome to Elevate - Please confirm your e-mail address.";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            smtpClient.Send(mailMessage);

        }
    }
}
