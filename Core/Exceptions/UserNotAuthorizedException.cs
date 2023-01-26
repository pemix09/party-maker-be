namespace Persistence.Exceptions
{
    public class UserNotAuthorizedException : BaseAppException
    {
        private int exceptionCode = 403;
        private string exceptionMessage = "User is not authorized to access the resource";

        public UserNotAuthorizedException() : base()
        {
            code = exceptionCode;
            message = exceptionMessage;
        }
    }
}
