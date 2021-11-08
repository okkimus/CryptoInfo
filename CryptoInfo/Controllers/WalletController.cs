using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Wallets.Commands.AddWallet;
using Application.Wallets.Queries.GetWalletByName;
using Application.Wallets.Queries.GetWallets;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    // [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("Wallet")]

        public async Task<ActionResult<List<Wallet>>> GetWallets()
        {
            var wallets = await _mediator.Send(new GetWalletsQuery());
            return Ok(wallets);   
        }
        
        [HttpGet]
        [Route("Wallet/Name/{name?}")]
        public async Task<ActionResult<List<Wallet>>> GetWalletByName(string name)
        {
            var wallet = await _mediator.Send(new GetWalletByNameQuery { Name = name });
            return Ok(wallet);   
        }

        [HttpPost]
        [Route("Wallet")]
        public async Task<ActionResult<Wallet>> AddWallet([FromBody] WalletParams parameters)
        {
            // TODO: Swap out the hard coded values for real parameters
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