using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using wallet_microservice_dotnet._1.Domain.Entities;

namespace wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces
{
    public interface IWalletService
    {
        Task UpdateWalletAsync(long walletId, long amount);
        Task UpdateWalletAsync(WalletEntity wallet, long amount);
        Task<WalletEntity> CreateWalletAsync(long walletId);
        Task<WalletEntity> GetWalletAsync(long walletId);
    }
}
