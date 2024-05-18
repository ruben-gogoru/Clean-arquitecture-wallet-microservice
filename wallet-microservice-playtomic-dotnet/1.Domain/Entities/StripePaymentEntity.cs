using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace wallet_microservice_dotnet._1.Domain.Entities
{
    public class StripePaymentEntity : CreateUpdateAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? ExternalPaymentId {  get; set; }

        public String CreditCard {  get; set; }

        public String Status { get; set; }

        public long WalletId { get; set; }
        public RelationExtPaymentWithTransactionEntity Relation { get; set; }

    }
}
