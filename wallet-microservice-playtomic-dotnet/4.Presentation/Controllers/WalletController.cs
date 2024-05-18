using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Numerics;
using wallet_microservice_dotnet._2.Application.UseCases;
using wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_dotnet._4.Presentation.Models;
using wallet_microservice_playtomic_dotnet._4.Presentation.Models;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace wallet_microservice_dotnet._4.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly WalletUseCase _walletUseCase;
        private readonly IMapper _mapper;

        public WalletController(WalletUseCase walletUseCase, IMapper mapper)
        {
            _walletUseCase = walletUseCase;
            _mapper = mapper;
        }

        [HttpGet("{walletId}")]
        [Consumes("application/json")]

        public async Task<ActionResult> GetWalletAsync([FromRoute] long walletId)
        {
            
            var walletEntity = await _walletUseCase.GetWalletAsync( walletId);

            var walletDto = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(walletDto);
            
        }

        [HttpPost("{walletId}")]
        [Consumes("application/json")]

        public async Task<ActionResult> CreateWalletAsync([FromBody] long walletId)
        {
            
            var walletEntity = await _walletUseCase.CreateWalletAsync(walletId);

            var walletDto = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(walletDto);
            
        }

        [HttpPost("charge")]
        [Consumes("application/json")]
        
        public async Task<IActionResult> ChargeWalletAsync([FromBody]  ChargeWalletDTO chargeWalletDTO)
        {
            var walletEntity = await _walletUseCase.ChargeWalletAsync(chargeWalletDTO.WalletId, chargeWalletDTO.Amount, chargeWalletDTO.CreditCardNumber);
            var wallet = _mapper.Map<WalletDTO>(walletEntity);
            return Ok();
        }

        

        
    }
}
