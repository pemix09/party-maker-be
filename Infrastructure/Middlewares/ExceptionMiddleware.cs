using Infrastructure.Exceptions;
using Persistence.Exceptions;
using System.Net;
using FluentValidation;

namespace Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> _logger,RequestDelegate _next)
        {
            next = _next;
            logger = _logger;
        }

        public async Task InvokeAsync(HttpContext _httpContext)
        {
            //if everything is cool, we can pass the context to next middleware
            try
            {
                await next(_httpContext);
            }
            //catch exception created in this app(custom exceptions)
            catch (BaseAppException _appEx)
            {
                logger.LogError($"Something went wrong: {_appEx.Message}");
                await HandleExceptionAsync(_httpContext, _appEx.GetExceptionMessage(), _appEx.GetExceptionCode());
            }
            //catch validation exceptions
            catch (ValidationException _valEx)
            {
                await HandleExceptionAsync(_httpContext, _valEx.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex.InnerException);
                logger.LogError($"Something went wrong: {_ex.Message}");
                await HandleExceptionAsync(_httpContext, _ex.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext _httpContext, string _message, int _code = (int)HttpStatusCode.InternalServerError)
        {
            _httpContext.Response.ContentType = "application/json";
            _httpContext.Response.StatusCode = _code;

            ErrorDetails errorDetails = new ErrorDetails(_httpContext.Response.StatusCode, _message);

            return _httpContext.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
