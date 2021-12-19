using System;

namespace QuestionManager.BLL.Configuration
{
    public class SmtpClientConfiguration
    {
        public string SenderAddres { get; set; }
        public string SenderPassword { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}
