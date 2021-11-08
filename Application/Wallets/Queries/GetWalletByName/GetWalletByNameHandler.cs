using System.Threading;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWalletByName
{
    public class GetWalletByNameHandler : IRequestHandler<GetWalletByNameQuery, Wallet>
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private readonly IBalanceService _balanceService;

        public GetWalletByNameHandler(IWalletService walletService, ITransactionService transactionService, IBalanceService balanceService)
        {
            _walletService = walletService;
            _transactionService = transactionService;
            _balanceService = balanceService;
        }

        public async Task<Wallet> Handle(GetWalletByNameQuery request, CancellationToken cancellationToken)
        {
            var wallet = _walletService.GetWalletByName(request.Name);
            wallet.Transactions = await _transactionService.GetTransactionsByWalletAddressAsync(wallet.Address.Value);
            wallet.Balances = _balanceService.CalculateBalances(wallet);

            return wallet;
        }
    }
}