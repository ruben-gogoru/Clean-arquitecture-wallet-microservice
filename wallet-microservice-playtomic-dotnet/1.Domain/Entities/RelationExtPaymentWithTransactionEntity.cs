using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace wallet_microservice_dotnet._1.Domain.Entities
{
    public class RelationExtPaymentWithTransactionEntity : CreateUpdateAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long WalletTransactionId { get; set; }
        public long StripePaymentId { get; set; }   

        public virtual WalletTransactionsEntity WalletTransaction { get; set; }
        public virtual StripePaymentEntity StripePayment { get; set; }
    }
}
