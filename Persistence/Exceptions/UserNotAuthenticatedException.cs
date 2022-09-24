namespace Persistence.Exceptions
{
    public class UserNotAuthenticatedException : BaseAppException
    {
        private const int exceptionCode = 401;
        private const string exceptionMessage = "User is not authenticated to access the resource";

        public UserNotAuthenticatedException(string _msg = exceptionMessage):base()
        {
            code = exceptionCode;
            message = _msg;
        }
    }
}
