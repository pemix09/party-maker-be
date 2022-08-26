﻿namespace Persistence.Exceptions
{
    public class UserNotFoundException : BaseAppException
    {
        public UserNotFoundException(string _msg = "User haven't been found", int _code = 404) : base(_msg)
        {
            message = _msg;
            code = _code;
        }
    }
}
