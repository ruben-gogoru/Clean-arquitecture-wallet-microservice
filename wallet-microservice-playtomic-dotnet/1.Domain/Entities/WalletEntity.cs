using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace wallet_microservice_dotnet._1.Domain.Entities
{
    public class WalletEntity : CreateUpdateAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Balance { get; set; } = 0;

        public virtual List<WalletTransactionsEntity> WalletTransaction { get; set; }
        public virtual List<StripePaymentEntity> ExternalPayment { get; set; }


    }
}
