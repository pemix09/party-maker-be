namespace Persistence.Exceptions
{
    public class UserNotFoundException : BaseAppException
    {
        public UserNotFoundException(string _email, int _code = 404) : base()
        {
            message = $"User with email: {_email} have not been found!";
            code = _code;
        }
    }
}
