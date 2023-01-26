namespace Persistence.Exceptions;

public class InvalidRefreshTokenException : BaseAppException
{
    private const string exMsg = "Invalid refresh token";
    private const int exCode = StatusCodes.Status401Unauthorized;

    public InvalidRefreshTokenException() : base()
    {
        code = exCode;
        message = exMsg;
    }
}