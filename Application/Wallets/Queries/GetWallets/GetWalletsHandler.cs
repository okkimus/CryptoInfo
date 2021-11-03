using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWallets
{
    public class GetWalletsHandler : IRequestHandler<GetWalletsQuery, List<Wallet>>
    {
        private readonly IWalletService _walletService;

        public GetWalletsHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<List<Wallet>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
        {
            var wallets = await _walletService.GetWalletsAsync();
            return wallets;
        }
    }
}
