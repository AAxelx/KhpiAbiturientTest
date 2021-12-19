using Newtonsoft.Json;
using QuestionManager.BLL.Configuration;
using QuestionManager.BLL.Services.Abstractions;
using System.IO;

namespace QuestionManager.BLL.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public LetterConfiguration GetLetterConfiguration()
        {
            var configFile = File.ReadAllText("configuration.json");
            var config = JsonConvert.DeserializeObject<LetterConfiguration>(configFile);
            return config;
        }

        public MessageConfiguration GetMessageConfiguration()
        {
            var config = GetLetterConfiguration().MessageConfiguration;
            return config;
        }

        public SmtpClientConfiguration GetSmtpClientConfiguration()
        {
            var config = GetLetterConfiguration().SmtpClientConfiguration;
            return config;
        }
    }
}
