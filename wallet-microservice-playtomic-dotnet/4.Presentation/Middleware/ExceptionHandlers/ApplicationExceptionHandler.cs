using System.Net;

namespace wallet_microservice_dotnet._4.Presentation.Middleware
{
    public class ApplicationExceptionHandler : BaseExceptionHandlerAbstract<ApplicationException>
    {
        public ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger) : base(logger)
        {

        }

        public override int GetHttpStatusCode(HttpContext context)
        {
            return (int)HttpStatusCode.InternalServerError;
        }

    }
}
