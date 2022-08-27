namespace Persistence.Exceptions
{
    public class UserCannotBeSignInException : BaseAppException
    {
        public UserCannotBeSignInException(string _userName, int _code = 401) : base()
        {
            message = $"{_userName} cannot be signed in!";
            code = _code;
        }
    }
}
