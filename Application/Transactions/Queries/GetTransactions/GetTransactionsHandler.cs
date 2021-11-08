using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;
using MediatR;

namespace Application.Transactions.Queries
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionsHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionService.GetTransactionsAsync();
            
            return transactions;
        }
    }
}