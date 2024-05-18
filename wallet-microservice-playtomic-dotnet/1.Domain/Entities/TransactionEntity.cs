using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection;

namespace wallet_microservice_playtomic_dotnet._1.Domain.Entities
{
    public class TransactionEntity : CreateUpdateAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long WalleId { get; set; }

        public long Amount { get; set; }
        public String TransactionType { get; set; }

        //public string GenderString
        //{
        //    get { return Gender.ToString(); }
        //    private set { Gender = EnumExtensions.ParseEnum<Gender>(value); }
        //}

        public virtual WalletEntity Wallet { get; set; }
        public virtual RelationExtPaymentWithTransactionEntity Relation { get; set; }

    }
}
