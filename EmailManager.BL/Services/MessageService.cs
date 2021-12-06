using EmailManager.BL.Abstractions;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfigurationService _configurationService;

        public MessageService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        // Принимает эмейл адрес получателя и его полученные баллы
        // Возвращает объект Mimemessage с настройками для SMTP клиента
        // Используется нугет пакет MimeKit
        public MimeMessage CreateMessageDetails(string receiverAddres, int score)
        {
            var messageConfig = _configurationService.GetMessageConfiguration();

            var htmlBody = messageConfig.BodyFirstPart + score + messageConfig.BodySecondPart;

            var receiverParsedAddres = MailboxAddress.Parse(receiverAddres);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(messageConfig.SenderName, messageConfig.SenderAddres));
            message.To.Add(receiverParsedAddres);
            message.Subject = messageConfig.Subject;
            message.Body = new BodyBuilder() { HtmlBody = htmlBody }.ToMessageBody();

            return message;
        }
    }
}
