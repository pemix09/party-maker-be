namespace Persistence.Exceptions;

public class TokenExpiredException : BaseAppException
{
    private const string exMsg = "Refresh token expired, log in again!";
    private const int exCode = StatusCodes.Status401Unauthorized;

    public TokenExpiredException() : base()
    {
        code = exCode;
        message = exMsg;
    }
}