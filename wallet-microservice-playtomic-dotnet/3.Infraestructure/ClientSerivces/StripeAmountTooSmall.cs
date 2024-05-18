namespace wallet_microservice_dotnet._3.Infraestructure.ClientSerivces
{
    public class StripeAmountTooSmall : ApplicationException
    {
        public StripeAmountTooSmall(string message) : base(message){ }
    }
}
