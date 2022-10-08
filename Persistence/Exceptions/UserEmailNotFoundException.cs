namespace Persistence.Exceptions
{
    public class UserEmailNotFoundException : BaseAppException
    {
        private const int exCode = StatusCodes.Status404NotFound;
        public UserEmailNotFoundException(string _email) : base()
        {
            message = $"User with email: {_email} have not been found!";
            code = exCode;
        }
        
    }
}
