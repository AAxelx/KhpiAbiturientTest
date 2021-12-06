using EmailManager.BL.Abstractions;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace EmailManager.BL.Services
{
    public class LetterService : ILetterService
    {
        private readonly IConfigurationService _configurationService;

        public LetterService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        // Принимает обьект MimeMessage
        // Возвращает истину если письмо было отправлено, ложь - если при отправке возникли ошибки.
        // Используется нугет пакет MimeKit
        public async Task<bool> SendAsync(MimeMessage message)
        {
            try
            {
                var smtpConfig = _configurationService.GetSmtpClientConfiguration();

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpConfig.Host, smtpConfig.Port, smtpConfig.UseSSL);
                    client.Authenticate(smtpConfig.SenderAddres, smtpConfig.SenderPassword);
                    await client.SendAsync(message);

                    client.Disconnect(true);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}