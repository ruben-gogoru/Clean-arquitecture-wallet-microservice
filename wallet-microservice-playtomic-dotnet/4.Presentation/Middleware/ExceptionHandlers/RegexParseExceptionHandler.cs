

using System.Net;
using System.Text.RegularExpressions;

namespace wallet_microservice_dotnet._4.Presentation.Middleware
{
    public class RegexParseExceptionHandler : BaseExceptionHandlerAbstract<RegexParseException>
    {
        public RegexParseExceptionHandler(ILogger<RegexParseExceptionHandler> logger) : base(logger)
        {
        }

        public override int GetHttpStatusCode(HttpContext context)
        {
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
