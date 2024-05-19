using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection;

namespace wallet_microservice_dotnet._1.Domain.Entities
{
    public class WalletTransactionsEntity : CreateUpdateAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long WalleId { get; set; }

        public long Amount { get; set; }
        public string PaymentId { get; set; }

        [EnumDataType(typeof(WalletTransactionTypeEnum))]
        public WalletTransactionTypeEnum TransactionType { get; set; }
        
        public virtual WalletEntity Wallet { get; set; }

    }
}
