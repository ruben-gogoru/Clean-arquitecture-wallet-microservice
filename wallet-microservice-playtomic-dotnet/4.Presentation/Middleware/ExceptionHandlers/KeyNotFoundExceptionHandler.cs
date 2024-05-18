

using System.Net;

namespace wallet_microservice_dotnet._4.Presentation.Middleware
{
    public class KeyNotFoundExceptionHandler : BaseExceptionHandlerAbstract<KeyNotFoundException>
    {
        public KeyNotFoundExceptionHandler(ILogger<KeyNotFoundExceptionHandler> logger) : base(logger)
        {
        }

        public override int GetHttpStatusCode(HttpContext context)
        {
            return (int) HttpStatusCode.NotFound;
        }
    }
}
