using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ImportController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Transactions(IFormFile file)
        {
            // Pass the file for a CSV file handler
            // Save the tx to DB
            // Return number of imported txs
            
            // string fileContents;
            // using (var stream = file.OpenReadStream())
            // using (var reader = new StreamReader(stream))
            // {
            //     fileContents = await reader.ReadToEndAsync();
            // }
            return Ok("");
        }
    }
}