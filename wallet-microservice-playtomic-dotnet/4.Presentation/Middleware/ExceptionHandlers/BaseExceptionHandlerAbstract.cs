using Newtonsoft.Json;
using System.Net;

namespace wallet_microservice_dotnet._4.Presentation.Middleware
{
    public abstract class BaseExceptionHandlerAbstract<T> : IErrorHandler  where T : Exception
    {
        protected readonly ILogger _logger;

        protected BaseExceptionHandlerAbstract(ILogger logger)
        {
            _logger = logger;
        }

        public async Task HandleError(HttpContext context, Exception error)
        {
            //registired as information as this exception is controlled
            _logger.LogInformation(error, error.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetHttpStatusCode(context);            
            var result = JsonConvert.SerializeObject(error.Message);
            await context.Response.WriteAsync(result);
        }

        public abstract int GetHttpStatusCode(HttpContext context);
    }
}
