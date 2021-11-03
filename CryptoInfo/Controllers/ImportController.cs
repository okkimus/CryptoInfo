using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Importers.CsvTxImporter.ImportTxs;
using Domain;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ImportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<List<Transaction>>> Transactions(IFormFile txFile, IFormFile transferFile)
        {
            var transactions = new List<Transaction>();
            
            using (var txStream = new MemoryStream())
            using (var transferStream = new MemoryStream())
            {
                await txFile.CopyToAsync(txStream);
                await transferFile.CopyToAsync(transferStream);
                transactions = await _mediator.Send(new ImportTxsCommand
                {
                    Options = new CsvImporterOptions
                    {
                        TxCsv = txStream,
                        TransferCsv = transferStream
                    }
                });
            }
            
            return Ok(transactions);
        }
    }
}