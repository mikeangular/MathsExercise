using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace MathsExercise.Tools
{
    public static class MailManagement{
        public static string SenderName = "Maths Exercises";
        public static string SenderEmail = "wang8014@aliyun.com";
        public static string EmailPassword = "whglxy4693419";
        public static string SmtpHost = "smtp.aliyun.com";
        public static int SmtpPort = 465;

        public static string recipientEmail = "wang8014@gmail.com";

        public static void Send(string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(MailManagement.SenderName, MailManagement.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("mail", MailManagement.recipientEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect(MailManagement.SmtpHost, MailManagement.SmtpPort, true);
                client.Authenticate(MailManagement.SenderEmail, MailManagement.EmailPassword);

                client.Send(emailMessage);
                client.Disconnect(true);

            }
        }

        public static async Task SendEmailAsync(string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(MailManagement.SenderName, MailManagement.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("mail", MailManagement.recipientEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(MailManagement.SmtpHost,MailManagement.SmtpPort, SecureSocketOptions.None).ConfigureAwait(false);
                await client.AuthenticateAsync(MailManagement.SenderName,MailManagement.EmailPassword);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
        }
    }
}