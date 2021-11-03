using System.Collections.Generic;
using Domain;

namespace Application.ServiceAbstractions
{
    public interface ITransactionImporter
    {
        List<Transaction> ReadTransactions(ITransactionImporterOptions options);
    }
    
    public interface ITransactionImporterOptions {}
}