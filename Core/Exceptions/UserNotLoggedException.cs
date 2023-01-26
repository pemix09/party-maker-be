namespace Persistence.Exceptions
{
    public class UserNotLoggedException : BaseAppException
    {
        public UserNotLoggedException(string _msg="User is not logged!", int _code = 404) : base()
        {
            message = _msg;
            code = _code;
        }
    }
}
