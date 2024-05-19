using wallet_microservice_dotnet._1.Domain.Entities;

namespace wallet_microservice_dotnet._2.Application.Services
{
    public interface IWalletTransactionService
    {
        Task<WalletTransactionsEntity> CreateWalletTranactionAsync(long walletId,
            long amount, WalletTransactionTypeEnum type, string payment);

        Task<List<WalletTransactionsEntity>> GetWalletTransactions(long walletId);
    }
}