namespace Persistence.Exceptions;

public class UserIdNotFoundException : BaseAppException
{
    private const int exCode = StatusCodes.Status404NotFound;
    
    public UserIdNotFoundException(string _Id) : base()
    {
        message = $"User with given id: {_Id} was not found";
        code = exCode;
    }
}