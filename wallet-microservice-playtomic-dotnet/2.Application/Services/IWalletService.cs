using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using wallet_microservice_playtomic_dotnet._1.Domain.Entities;

namespace wallet_microservice_playtomic_dotnet._3.Infraestructure.ServiceInterfaces
{
    public interface IWalletService
    {
        public void TopUpWalletAsync(long walletId, long amount, [NotNull] string CreditCard);
        Task<WalletEntity> CreateWalletAsync(long walletId);
    }
}
