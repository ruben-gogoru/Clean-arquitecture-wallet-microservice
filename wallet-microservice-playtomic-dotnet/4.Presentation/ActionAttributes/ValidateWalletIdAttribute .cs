using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace wallet_microservice_dotnet._4.Presentation.ActionAttributes
{
    public class ValidateWalletIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("walletId"))
            {
                var walletId = (long)context.ActionArguments["walletId"];
                if (walletId < 1)
                {
                    context.Result = new BadRequestObjectResult("walletId must be greater than or equal to 1.");
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
