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

        public ImportTxsHandler(ITransactionImporter importer)
        {
            _importer = importer;
        }

        public Task<List<Transaction>> Handle(ImportTxsCommand request, CancellationToken cancellationToken)
        {
            var transactions = _importer.ReadTransactions(request.Options);
            
            // TODO: Save them in memory place
            
            return Task.FromResult(transactions);
        }
    }
}