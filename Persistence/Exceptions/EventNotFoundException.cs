namespace Persistence.Exceptions
{
    public class EventNotFoundException : BaseAppException
    {
        const string msg = "Event not found!";
        const int statusCode = StatusCodes.Status404NotFound;
        public EventNotFoundException() : base() 
        {
            base.message = msg;
            base.code= statusCode;
        }
    }
}
