using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Configuration
{
    public class MessageConfiguration
    { 
        public string SenderAddres { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string BodyFirstPart { get; set; }
        public string BodySecondPart { get; set; }
    }
}
