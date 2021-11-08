using System.Collections.Generic;
using Domain;
using MediatR;

namespace Application.Transactions.Queries
{
    public class GetTransactionsQuery : IRequest<List<Transaction>> { }
}