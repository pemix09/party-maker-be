using Newtonsoft.Json;

namespace Infrastructure.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode;
        public string Message;
        public ErrorDetails(int _statusCode, string _message)
        {
            StatusCode = _statusCode;
            Message = _message;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
