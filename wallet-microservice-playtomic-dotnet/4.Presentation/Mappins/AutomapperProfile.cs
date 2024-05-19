using AutoMapper;
using wallet_microservice_dotnet._1.Domain.Entities;
using wallet_microservice_dotnet._4.Presentation.Models;
using wallet_microservice_dotnet._4.Presentation.ModelsDTOs;

namespace wallet_microservice_dotnet._2.Application.Behaviours
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<WalletEntity, WalletDTO>();
            CreateMap<WalletTransactionsEntity, WalletTransactionDTO>();
        }
    }
}
