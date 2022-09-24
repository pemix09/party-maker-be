namespace Persistence.Exceptions
{
    public class UserNotAuthenticatedException : BaseAppException
    {
        private const int exceptionCode = 401;
        private const string exceptionMessage = "User is not authenticated to access the resource";
        private const string panicMessage = "Some error with authentication occured";

        public UserNotAuthenticatedException(string _msg = exceptionMessage):base()
        {
            code = exceptionCode;

            if(_msg != null)
            {
                message = _msg;
            }
            else
            {
                message = panicMessage;
            }
        }
    }
}
