using QuestionManager.BLL.Configuration;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IConfigurationService
    {
        LetterConfiguration GetLetterConfiguration();
        public MessageConfiguration GetMessageConfiguration();
        public SmtpClientConfiguration GetSmtpClientConfiguration();
    }
}
