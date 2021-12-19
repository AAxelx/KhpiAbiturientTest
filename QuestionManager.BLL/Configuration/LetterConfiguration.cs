using System;

namespace QuestionManager.BLL.Configuration
{
    public class LetterConfiguration
    {
        public MessageConfiguration MessageConfiguration { get; set; }
        public SmtpClientConfiguration SmtpClientConfiguration { get; set; }
    }
}
