using System.Collections.Generic;
using System.IO;
using Application.ServiceAbstractions;
using Domain;
using MediatR;

namespace Application.Importers.CsvTxImporter.ImportTxs
{
    public class ImportTxsCommand : IRequest<List<Transaction>>
    {
        public ITransactionImporterOptions Options { get; set; }
    }
}