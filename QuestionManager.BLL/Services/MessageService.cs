using MimeKit;
using QuestionManager.BLL.Services.Abstractions;

namespace QuestionManager.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfigurationService _configurationService;

        public MessageService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public MimeMessage CreateMessageDetails(string receiverAddres, string textBody)
        {
            var messageConfig = _configurationService.GetMessageConfiguration();

            var receiverParsedAddres = MailboxAddress.Parse(receiverAddres);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(messageConfig.SenderName, messageConfig.SenderAddres));
            message.To.Add(receiverParsedAddres);
            message.Subject = messageConfig.Subject;
            message.Body = new BodyBuilder() { TextBody = textBody }.ToMessageBody();

            return message;
        }
    }
}
