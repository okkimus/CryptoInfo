using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWalletByAddress
{
    public class GetWalletByAddressHandler : IRequestHandler<GetWalletByAddressQuery, Wallet>
    {
        public Task<Wallet> Handle(GetWalletByAddressQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}