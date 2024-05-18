using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using wallet_microservice_playtomic_dotnet._2.Application.UseCases;
using wallet_microservice_playtomic_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_playtomic_dotnet._4.Presentation.Models;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace wallet_microservice_playtomic_dotnet._4.Presentation.Controllers
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

        [HttpPost("topup")]
        [Consumes("application/json")]
        
        public async Task<IActionResult> TopUpAsync([FromBody]  string journey)
        {
            //var result = await _journeyService.RequestJourney(journey);
            return null;
        }

        [HttpPost("{walletId}")]
        [Consumes("application/json")]

        public async Task<ActionResult> CreateWalletAsync([FromRoute] long walletId)
        {
            var walletEntity = await _walletUseCase.CreateWalletAsync(walletId);

            var walletDto = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(walletDto);
        }

        //https://localhost:xxxxx/api/v1/ShelfID/{shelfID}/BookCollection?ID="123"&Name="HarryPotter"
        //[HttpGet("example")]
        //public async Task<IActionResult> GetAllBooks(string shelfID,
        //                                     [FromQuery] string ID,
        //                                     [FromQuery] string Name)
        //{

        //}
    }
}
