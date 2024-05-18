using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;


namespace wallet_microservice_dotnet._2.Application.Services
{
    public class StripeService
    {
        private readonly HttpClient _httpClient;

        public StripeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentModel>  Charge([NotNull] String creditCard, [NotNull] long amount)
        {

            var model = new
            {
                credit_card = creditCard,
                amount = amount.ToString()
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("charges", content);
                        
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaymentModel>(stringResponse);
            
            

        }

        public async Task<Object> Refund([NotNull] String paymentId)
        {
            var model = JsonConvert.SerializeObject(paymentId);
            //we don't need body in this example
            var response = await _httpClient.PostAsync($"refund/{paymentId}/refund", null);
            if(response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                var resultObject = JsonConvert.DeserializeObject<Object>(contentString);
                return resultObject;
                 
            }
            else
            {
                throw new Exception("Stripe service failed");
            }
        }

    }
}
