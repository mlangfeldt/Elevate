using System.Net;
using System.Net.Mail;
using System.Web;

namespace Elevate.BL
{
    public class EmailService
    {
        public static void SendResetCodeEmail(string email, string code)
        {
            string resetUrl = $"https://yourwebsite.com/User/ResetPassword?email={HttpUtility.UrlEncode(email)}";
            string body = $"Your password reset code is: {code}\n\n" +
                          $"Please click the link below to reset your password:\n{resetUrl}";


            var smtpClient = new SmtpClient("smtp.yourserver.com")
            {
                // 7135
                Port = 587,
                Credentials = new NetworkCredential("your_email@example.com", "your_email_password"),
                EnableSsl = true,
            };

            smtpClient.Send("your_email@example.com", email, resetUrl, body);

        }

    }
}
