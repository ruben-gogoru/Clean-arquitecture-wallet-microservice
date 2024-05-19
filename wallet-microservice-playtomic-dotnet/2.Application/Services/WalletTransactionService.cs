using wallet_microservice_dotnet._1.Domain.Entities;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;
using wallet_microservice_dotnet._3.Infraestructure;
using WebApi.Helpers;

namespace wallet_microservice_dotnet._2.Application.Services
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly IGenericRepository<WalletTransactionsEntity> _walletTransactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletTransactionService(IGenericRepository<WalletTransactionsEntity> _walletTransactionRepository,
            IUnitOfWork unitOfWork)
        {
            this._walletTransactionRepository = _walletTransactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WalletTransactionsEntity> CreateWalletTranactionAsync(long walletId,
            long amount, WalletTransactionTypeEnum type, string paymentId)
        {          

            var transaction = new WalletTransactionsEntity()
            {
                TransactionType = type,
                Amount = amount,
                WalleId = walletId,
                PaymentId = paymentId
            };

            transaction = await _walletTransactionRepository.CreateAsync(transaction);

            if (transaction == null)
                throw new AppException("Could not create the walletTransaction");

            return transaction;
        }

        public async Task<List<WalletTransactionsEntity>> GetWalletTransactions(long walletId)
        {
            var transactionsList = await _walletTransactionRepository.GetAsync(x => x.WalleId == walletId);
            if (transactionsList == null)
                throw new KeyNotFoundException("Not found transactions history");

            return (List<WalletTransactionsEntity>) transactionsList;
        }

        
    }
}
