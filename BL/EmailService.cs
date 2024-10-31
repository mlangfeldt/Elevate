using System.Net;
using System.Net.Mail;

namespace Elevate.BL
{
    public class EmailService
    {
        public static void SendResetCodeEmail(string email, string code)
        {
            string body = $"Your password reset code is: {code}";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                // 7135
                Port = 587,
                Credentials = new NetworkCredential("knudtdaw0000@gmail.com", "uesf riic xrrl omgs"),
                EnableSsl = true,
            };

            smtpClient.Send("knudtdaw0000@gmail.com", email, "Reset Password Code", body);

        }

    }
}
