using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace Nhom17_BaiTapLon_WebBanCayCanh.Services
{
    public class MailSender
    {
        public static void SendMail(string from, string to, string subject, string text)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = text
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("son286202@gmail.com", "mmnzspjalndumomu");
                smtp.Send(message);
                smtp.Disconnect(true);
            }
        }
    }
}
