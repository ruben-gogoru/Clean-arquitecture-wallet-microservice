using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using wallet_microservice_dotnet._1.Domain.Entities;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;
using wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_dotnet._4.Presentation.Models;
using WebApi.Helpers;

namespace wallet_microservice_dotnet._2.Application.Services
{
    public class WalletService : IWalletService
    {
        
        private readonly IGenericRepository<WalletEntity> _walletRepository;

        public WalletService(
            IGenericRepository<WalletEntity> walletRepository) 
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletEntity> GetWalletAsync(long walletId)
        {
            WalletEntity wallet = await _walletRepository.FirstAsync(x => x.Id == walletId);            

            return wallet;
        }

        public async Task<WalletEntity> CreateWalletAsync(long walletId)
        {
            var wallet = await GetWalletAsync(walletId);
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

            return wallet;
        }

        public async Task UpdateWalletAsync(WalletEntity wallet, long amount)
        {
            wallet.Balance = wallet.Balance + amount;

        }

        public async Task UpdateWalletAsync(long walletId, long amount)
        {
            WalletEntity wallet = await GetWalletAsync(walletId);
            if (wallet == null)
            {
                throw new KeyNotFoundException();
            }

            UpdateWalletAsync(wallet, amount);

        }

    }
}
