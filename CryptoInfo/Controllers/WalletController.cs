using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Application.Wallets.Commands.AddWallet;
using Application.Wallets.Queries.GetWallets;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Wallet>>> GetWallets()
        {
            var wallets = await _mediator.Send(new GetWalletsQuery());
            return Ok(wallets);   
        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> AddWallet([FromBody] WalletParams parameters)
        {
            var walletToAdd = new Wallet(Network.Fantom, new Address(parameters.Address, AddressType.Wallet),
                parameters.Name);
            var command = new AddWalletCommand(walletToAdd);
            var createdWallet = await _mediator.Send(command);
            
            return Ok(createdWallet);   
        }
    }

    public class WalletParams
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }
}