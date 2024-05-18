using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace wallet_microservice_playtomic_dotnet._4.Presentation.Models
{
    public class WalletDTO
    {
        [Required(ErrorMessage = "WalletId is required")]
        [Range(1, long.MaxValue, ErrorMessage = "WalletId must be a positive number")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Balance is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Balance must be a positive number")]
        public long Balance { get; set; }
        
        public DateTime Updated { get; set; }

    }
}
