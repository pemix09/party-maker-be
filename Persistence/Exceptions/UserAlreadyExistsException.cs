namespace Persistence.Exceptions
{
    public class UserAlreadyExistsException : BaseAppException
    {
        public UserAlreadyExistsException(string _msg = "User with given email already exists!", int _code = 404) : base()
        {
            message = _msg;
            code = _code;
        }
    }
}
