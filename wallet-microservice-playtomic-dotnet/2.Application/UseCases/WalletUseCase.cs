using wallet_microservice_playtomic_dotnet._1.Domain.Entities;
using wallet_microservice_playtomic_dotnet._3.Infraestructure.ServiceInterfaces;

namespace wallet_microservice_playtomic_dotnet._2.Application.UseCases
{
    public class WalletUseCase
    {
        private readonly IWalletService _walletService;

        public WalletUseCase(IWalletService walletService)
        {
            _walletService = walletService;
        }
        public async Task<WalletEntity> CreateWalletAsync(long walletId)
        {
            return await _walletService.CreateWalletAsync(walletId);
        }

        //public async Task TopUpWalletAsync(string userId, decimal amount)
        //{
        //    await _walletService.TopUpWalletAsync(userId, amount);
        //}

        //public async Task<decimal> GetWalletBalanceAsync(string userId)
        //{
        //    return await _walletService.GetWalletBalanceAsync(userId);
        //}
    }
}
