using EmailManager.BL.Abstractions;
using EmailManager.BL.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Services
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
