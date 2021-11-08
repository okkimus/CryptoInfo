using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;
using MediatR;

namespace Application.Importers.CsvTxImporter.ImportTxs
{
    public class ImportTxsHandler : IRequestHandler<ImportTxsCommand, List<Transaction>>
    {
        private readonly ITransactionImporter _importer;
        private readonly ITransactionService _transactionService;

        public ImportTxsHandler(ITransactionImporter importer, ITransactionService transactionService)
        {
            _importer = importer;
            _transactionService = transactionService;
        }

        public async Task<List<Transaction>> Handle(ImportTxsCommand request, CancellationToken cancellationToken)
        {
            var transactions = _importer.ReadTransactions(request.Options);
            var addedTxs = await _transactionService.AddTransactionsAsync(transactions);
            
            return addedTxs;
        }
    }
}