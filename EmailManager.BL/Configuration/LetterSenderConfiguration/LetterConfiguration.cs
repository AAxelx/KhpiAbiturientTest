using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Configuration
{
    public class LetterConfiguration
    {
        public MessageConfiguration MessageConfiguration { get; set; }
        public SmtpClientConfiguration SmtpClientConfiguration { get; set; }
    }
}
