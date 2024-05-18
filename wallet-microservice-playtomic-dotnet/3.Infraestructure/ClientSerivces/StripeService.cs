using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;


namespace wallet_microservice_playtomic_dotnet._2.Application.Services
{
    public class StripeService
    {
        public Uri ChargesUri { get; set; }

        public Uri RefundsUri { get; set; }

        public HttpClient HttpClient { get; set; }

        public async Task<PaymentModel>  Charge([NotNull] String creditCard, [NotNull] long amount)
        {

            var model = new
            {
                creditCardNumber = creditCard,
                amount = amount
            };
            var content = new StringContent(JsonConvert.SerializeObject(model));
            var response = await HttpClient.PostAsync("charges", content);

            if(response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaymentModel>(stringResponse);
            }
            else
            {
                throw new Exception("Stripe service failed");
            }

        }

        public async Task<Object> Refund([NotNull] String paymentId)
        {
            var model = JsonConvert.SerializeObject(paymentId);
            //we don't need body in this example
            var response = await HttpClient.PostAsync($"refund/{paymentId}/refund", null);
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
