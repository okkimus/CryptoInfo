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