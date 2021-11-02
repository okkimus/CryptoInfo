using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Wallets.Queries.GetWalletValue
{
    public class GetWalletValueHandler : IRequestHandler<GetWalletValueQuery, double>
    {
        public Task<double> Handle(GetWalletValueQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}