﻿using wallet_microservice_dotnet._1.Domain.Entities;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;
using wallet_microservice_dotnet._2.Application.Services;
using wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces;

namespace wallet_microservice_dotnet._2.Application.UseCases
{
    public class WalletUseCase
    {
        private readonly IWalletService _walletService;
        private StripeService _stripeService;
        private readonly IUnitOfWork _unitOfWork;

        public WalletUseCase(IWalletService walletService, 
            StripeService stripeService,
            IUnitOfWork unitOfWork)
        {
            _walletService = walletService;
            _stripeService = stripeService;
            _unitOfWork = unitOfWork;
        }

        public async Task<WalletEntity> GetWalletAsync(long walletId)
        {
            var wallet = await _walletService.GetWalletAsync(walletId);
            if (wallet == null)
                throw new KeyNotFoundException("Wallet not exists");

            return wallet;
        }

        public async Task<WalletEntity> CreateWalletAsync(long walletId)
        {
            var wallet = await _walletService.CreateWalletAsync(walletId);
            _unitOfWork.Commit<WalletEntity>();

            return wallet;
        }

        public async Task<WalletEntity> ChargeWalletAsync(long walletId, long amount, string creditcard)
        {
            var wallet = await _walletService.GetWalletAsync(walletId);
            if (wallet == null)
                throw new KeyNotFoundException("Wallet not exists");

            var payment = await _stripeService.Charge(creditcard, amount);

            await _walletService.UpdateWalletAsync(wallet, amount);
            _unitOfWork.Commit<WalletEntity>();
            return wallet;

        }

        //public async Task<decimal> GetWalletBalanceAsync(string userId)
        //{
        //    return await _walletService.GetWalletBalanceAsync(userId);
        //}
    }
}
