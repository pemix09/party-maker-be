namespace Persistence.Exceptions
{
    public class UserWithSameNickNameExistsException : BaseAppException
    {
        const string msg = "User with same nick name exists!";
        const int statusCode = StatusCodes.Status400BadRequest;
        public UserWithSameNickNameExistsException() : base() 
        {
            base.message = msg;
            base.code = statusCode;
        }
    }
}
