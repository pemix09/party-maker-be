using Infrastructure.Exceptions;
using Persistence.Exceptions;
using System.Net;

namespace Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        //we will have to add logger manager later
        //private readonly ILoggerManager logger;

        public ExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext _httpContext)
        {
            //if everything is cool, we can pass the context to next middleware
            try
            {
                await next(_httpContext);
            }
            catch(BaseAppException _appEx)
            {
                await HandleExceptionAsync(_httpContext, _appEx.GetExceptionMessage(), _appEx.GetExceptionCode());
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex.InnerException);
                //logger.LogError($"Something went wrong");
                await HandleExceptionAsync(_httpContext, _ex.Message);
            }
        }

        //method below should be other class that handles errors
        private Task HandleExceptionAsync(HttpContext _httpContext, string _message, int _code = (int)HttpStatusCode.InternalServerError)
        {
            _httpContext.Response.ContentType = "application/json";
            _httpContext.Response.StatusCode = _code;

            ErrorDetails errorDetails = new ErrorDetails(_httpContext.Response.StatusCode, _message);

            return _httpContext.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
