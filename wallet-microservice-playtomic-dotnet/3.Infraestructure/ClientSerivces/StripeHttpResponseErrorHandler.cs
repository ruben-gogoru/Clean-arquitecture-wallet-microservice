using System.Net;

namespace wallet_microservice_dotnet._3.Infraestructure.ClientSerivces
{
    public class StripeHttpResponseErrorHandler : DelegatingHandler
    {
        
        public StripeHttpResponseErrorHandler( )
        {
            
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                    throw new StripeAmountTooSmall("Stripe service failed: amount too small" + await response.Content.ReadAsStringAsync());
                else
                    throw new Exception("Stripe service failed:+ await response.Content.ReadAsStringAsync()");

            }

            // Devuelve la respuesta
            return response;
        }
        

    }
    
}
