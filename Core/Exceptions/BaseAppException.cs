﻿namespace Persistence.Exceptions
{
    //general class for all exceptions for this app
    public class BaseAppException : Exception
    {
        protected string message { get; set; }
        protected int code { get; set; }
        public BaseAppException() : base() {}
        public int GetExceptionCode()
        {
            return code;
        }
        public string GetExceptionMessage()
        {
            return message;
        }
    }
}
