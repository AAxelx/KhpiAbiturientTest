﻿using System;
using System.Globalization;

namespace QuestionManager.BLL.Helpers
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException()
               : base() { }

        public KeyNotFoundException(string message)
            : base(message) { }

        public KeyNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
