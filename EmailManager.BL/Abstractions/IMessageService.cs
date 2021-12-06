using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Abstractions
{
    public interface IMessageService
    {
        public MimeMessage CreateMessageDetails(string receiverAddres, int score);
    }
}
