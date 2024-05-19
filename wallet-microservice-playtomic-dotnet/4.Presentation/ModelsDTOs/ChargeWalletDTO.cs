using System.ComponentModel.DataAnnotations;

namespace wallet_microservice_dotnet._4.Presentation.Models
{
    public class ChargeWalletDTO
    {
        [Required(ErrorMessage = "Balance is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Balance must be a positive number")]
        public long WalletId {  get; set; }

        [Required(ErrorMessage = "Balance is " +
            "required")]
        [Range(1, long.MaxValue, ErrorMessage = "Balance must be a positive number")]
        public long Amount { get; set; }

        
        [Required]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|6(?:011|5[0-9]{2})[0-9]{12}|(?:2131|1800|35\d{3})\d{11})$"
                        , ErrorMessage = "The Credit Card field is not a valid credit card number.")]
        public string CreditCardNumber { get; set; }
    }
}
