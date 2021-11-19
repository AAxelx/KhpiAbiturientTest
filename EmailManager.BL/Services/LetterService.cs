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

        public async Task<bool> Send(string receiverAddres, int score)
        {
            try
            {
                var config = _configurationService.GetLetterConfiguration();
                var messageConfig = config.MessageConfiguration;

                var senderAddres = messageConfig.SenderAddres;
                var senderName = messageConfig.SenderName;
                var subject = messageConfig.Subject;
                var bodyFirstPart = messageConfig.BodyFirstPart;
                var bodySecondPart = messageConfig.BodySecondPart;
                var body = bodyFirstPart + score + bodySecondPart;
                var receiverParsedAddres = MailboxAddress.Parse(receiverAddres);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderAddres));
                message.To.Add(receiverParsedAddres);
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder();
                message.Body = new BodyBuilder() { HtmlBody = body }.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    var smtpConfig = config.SmtpClientConfiguration;

                    client.Connect(smtpConfig.Host, smtpConfig.Port, smtpConfig.UseSSL);
                    client.Authenticate(smtpConfig.SenderAddres, smtpConfig.SenderPassword);
                    await client.SendAsync(message);

                    client.Disconnect(true);
                }
                return true;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }
    }
}