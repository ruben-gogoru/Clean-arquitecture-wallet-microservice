using AutoMapper;
using wallet_microservice_playtomic_dotnet._1.Domain.Entities;
using wallet_microservice_playtomic_dotnet._4.Presentation.Models;

namespace wallet_microservice_playtomic_dotnet._2.Application.Behaviours
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<WalletEntity, WalletDTO>();
        }
    }
}
