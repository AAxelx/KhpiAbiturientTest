using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Configuration
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
