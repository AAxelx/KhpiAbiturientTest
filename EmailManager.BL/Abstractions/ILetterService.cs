﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Abstractions
{
    public interface ILetterService
    {
        public Task<bool> SendAsync(MimeMessage message);
    }
}
