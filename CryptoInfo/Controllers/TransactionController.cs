using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Importers.CsvTxImporter.ImportTxs;
using Application.Transactions.Queries;
using Domain;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            var transactions = await _mediator.Send(new GetTransactionsQuery());
            
            return Ok(transactions);
        }
    }
}