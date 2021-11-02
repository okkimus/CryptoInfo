using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWalletByName
{
    public class GetWalletByNameHandler : IRequestHandler<GetWalletByNameQuery, Wallet>
    {
        public Task<Wallet> Handle(GetWalletByNameQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}