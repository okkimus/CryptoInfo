using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Wallet>>> GetWallets()
        {
            var createdWallet = new Wallet(Network.Fantom, new Address("123", AddressType.Wallet), "hello1");
            var createdWallet2 = new Wallet(Network.Fantom, new Address("1234", AddressType.Wallet), "hello2");

            var wallets = new List<Wallet> { createdWallet, createdWallet2 };

            return Ok(wallets);   
        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> AddWallet([FromBody] WalletParams parameters)
        {
            var createdWallet = new Wallet(Network.Fantom, new Address(parameters.Address, AddressType.Wallet), parameters.Name);
            
            return Ok(createdWallet);   
        }
    }

    public class WalletParams
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }
}