using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using wallet_microservice_playtomic_dotnet._1.Domain.Entities;
using wallet_microservice_playtomic_dotnet._1.Domain.RepositoryInterfaces;
using wallet_microservice_playtomic_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_playtomic_dotnet._4.Presentation.Models;
using WebApi.Helpers;

namespace wallet_microservice_playtomic_dotnet._2.Application.Services
{
    public class WalletService : IWalletService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<WalletEntity> _walletRepository;

        public WalletService(
            IUnitOfWork unitOfWork, 
            IGenericRepository<WalletEntity> walletRepository) 
        {
            _unitOfWork = unitOfWork;
            _walletRepository = walletRepository;
        }

        public async Task<WalletEntity> CreateWalletAsync(long walletId)
        {
            WalletEntity wallet = await _walletRepository.FirstAsync(x => x.Id == walletId);
            if (wallet != null)
            {
                throw new AppException("Wallet already exists");
            }

            wallet = new WalletEntity()
            {
                Id = walletId
            };

            wallet = await _walletRepository.CreateAsync(wallet);

            if (wallet == null)
                throw new AppException("Could not create the wallet");

            _unitOfWork.Commit<WalletEntity>();
            return wallet;
        }

        public async void TopUpWalletAsync(long walletId, long amount, [NotNull] string CreditCard)
        {
            WalletEntity wallet = await _walletRepository.FirstAsync(x => x.Id == walletId);
            if (wallet == null)
            {
                throw new KeyNotFoundException();
            }

            //await _stripeService.Charge(CreditCard, amount);

            wallet.Balance = wallet.Balance + amount;
            _unitOfWork.Commit<WalletEntity>();

        }

    }
}
