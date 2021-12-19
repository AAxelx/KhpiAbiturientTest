using MailKit.Net.Smtp;
using MimeKit;
using QuestionManager.BLL.Services.Abstractions;

namespace QuestionManager.BLL.Services
{
    public class LetterService : ILetterService
    {
        private readonly IConfigurationService _configurationService;

        public LetterService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public bool Send(MimeMessage message)
        {
            var smtpConfig = _configurationService.GetSmtpClientConfiguration();

            using (var client = new SmtpClient())
            {
                client.Connect(smtpConfig.Host, smtpConfig.Port, smtpConfig.UseSSL);
                client.Authenticate(smtpConfig.SenderAddres, smtpConfig.SenderPassword);
                client.Send(message);

                client.Disconnect(true);
            }

            return true;
        }
    }
}
