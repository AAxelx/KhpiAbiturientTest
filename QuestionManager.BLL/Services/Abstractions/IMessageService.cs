using MimeKit;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IMessageService
    {
        public MimeMessage CreateMessageDetails(string receiverAddres, string textBody);
    }
}
