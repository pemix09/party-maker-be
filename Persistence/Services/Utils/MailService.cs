namespace Persistence.Services.Utils
{
    using MailKit.Net.Smtp;
    using MimeKit;
    using MimeKit.Text;

    public class MailService
    {
        private IConfiguration configuration;
        private string senderEmail;
        private string appName;
        public MailService(IConfiguration _configuration)
        {
            configuration = _configuration;

            senderEmail = configuration.GetSection("EmailSettings:EmailFrom").Value;
            appName = configuration.GetSection("AppName").Value;
        }

        public async Task SendAccountConfirmation(string _emailTo)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(senderEmail));
            email.To.Add(MailboxAddress.Parse(_emailTo));
            email.Subject = $"{appName} email confiramtion";
            email.Body = new TextPart(TextFormat.Html)
            {
                //TODO
                Text = "To confirm your account click this link:"
            };

            await SendEmail(email);
        }

        private async Task SendEmail(MimeMessage _email)
        {
            string smtpServerName = configuration.GetSection("EmailSettings:SmtpServer").Value;
            int smtpPort = Int32.Parse(configuration.GetSection("EmailSettings:SmtpPort").Value);
            string login = configuration.GetSection("EmailSettings:EmailFrom").Value;
            string password = configuration.GetSection("EmailSettings:Password").Value;
            using var smtpClient = new SmtpClient();

            try
            {
                smtpClient.Connect(smtpServerName, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(login, password);
                var res = await smtpClient.SendAsync(_email);
                smtpClient.Disconnect(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
