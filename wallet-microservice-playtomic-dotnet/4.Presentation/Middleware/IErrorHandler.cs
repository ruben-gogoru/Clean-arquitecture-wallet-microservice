namespace wallet_microservice_dotnet._4.Presentation.Middleware
{
    public interface IErrorHandler
    {
        Task HandleError(HttpContext context, Exception error);
    }
}
