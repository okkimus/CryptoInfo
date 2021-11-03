using System.Threading;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;
using Domain.Exceptions;
using MediatR;

namespace Application.Wallets.Commands.AddWallet
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, Wallet>
    {
        private readonly IWalletService _walletService;

        public AddWalletHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<Wallet> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = request.walletToAdd;

            ValidateWallet(wallet);

            if (WalletExists(wallet))
            {
                throw new WalletAlreadyExistsException(wallet.Name, WalletExistsType.Address);
            }
            
            var addedWallet = await _walletService.AddWalletAsync(request.walletToAdd);

            return addedWallet;
        }

        private bool WalletExists(Wallet wallet)
        {
            try
            {
                var existingWallet = _walletService.GetWalletByName(wallet.Name);

                return existingWallet != null &&
                       existingWallet.Address.Value == wallet.Address.Value &&
                       existingWallet.Network == wallet.Network;
            }
            catch (WalletNotFound)
            {
                return false;
            }
        }

        private static void ValidateWallet(Wallet wallet)
        {
            if (string.IsNullOrEmpty(wallet.Name))
            {
                throw new InvalidWalletDataException("Wallet name missing.");
            }

            if (string.IsNullOrEmpty(wallet.Address.Value))
            {
                throw new InvalidWalletDataException("Wallet address missing.");
            }

            if (wallet.Address.Type == AddressType.NotSet)
            {
                throw new InvalidWalletDataException("Wallet address type not set.");
            }

            if (wallet.Network == Network.NotSet)
            {
                throw new InvalidWalletDataException("Wallet network not set.");
            }
        }
    }
}