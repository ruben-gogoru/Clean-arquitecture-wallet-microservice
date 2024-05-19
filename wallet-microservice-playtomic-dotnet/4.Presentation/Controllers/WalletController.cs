using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Numerics;
using wallet_microservice_dotnet._2.Application.UseCases;
using wallet_microservice_dotnet._3.Infraestructure.ServiceInterfaces;
using wallet_microservice_dotnet._4.Presentation.ActionAttributes;
using wallet_microservice_dotnet._4.Presentation.Models;
using wallet_microservice_dotnet._4.Presentation.ModelsDTOs;
using wallet_microservice_dotnet._4.Presentation.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

        /// <summary>
        /// Get wallet info by Id.
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns>Return wallet info</returns>
        [ProducesResponseType(typeof(WalletDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO),400)]
        [HttpGet("{walletId}")]
        [Consumes("application/json")]
        [ValidateWalletId]

        public async Task<ActionResult> GetWalletAsync([FromRoute] long walletId)
        {
            
            var walletEntity = await _walletUseCase.GetWalletAsync( walletId);

            var walletDto = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(walletDto);
            
        }
        /// <summary>
        /// Create wallet given walletId.
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns>Return wallet info</returns>
        [ProducesResponseType(typeof(WalletDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [HttpPost("{walletId}")]
        [Consumes("application/json")]
        [ValidateWalletId]
        public async Task<ActionResult> CreateWalletAsync([FromBody] long walletId)
        {
            
            var walletEntity = await _walletUseCase.CreateWalletAsync(walletId);

            var walletDto = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(walletDto);
            
        }
        /// <summary>
        /// Wallet that we want to add money.
        /// </summary>
        /// <param name="chargeWalletDTO"></param>
        /// <returns>Return wallet info</returns>
        [ProducesResponseType(typeof(WalletDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [HttpPost("charge")]
        [Consumes("application/json")]
        
        public async Task<IActionResult> ChargeWalletAsync([FromBody]  ChargeWalletDTO chargeWalletDTO)
        {
            var walletEntity = await _walletUseCase.ChargeWalletAsync(chargeWalletDTO.WalletId, chargeWalletDTO.Amount, chargeWalletDTO.CreditCardNumber);
            var wallet = _mapper.Map<WalletDTO>(walletEntity);
            return Ok(wallet);
        }

        /// <summary>
        /// Get wallet transaction history.
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns>Return list of wallet transactions </returns>
        [ProducesResponseType(typeof(List<WalletTransactionDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [HttpPost("history")]
        [Consumes("application/json")]
        [ValidateWalletId]
        public async Task<IActionResult> GetWalletTransactionHistoy([FromRoute] long walletId)
        {
            var walletTransactionList = await _walletUseCase.GetWalletTransactionHistory(walletId);
            var walletTransactionDTOs = _mapper.Map<List<WalletTransactionDTO>>(walletTransactionList);
            return Ok(walletTransactionDTOs);
        }




    }
}
