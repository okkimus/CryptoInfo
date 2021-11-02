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
        private IWalletService _walletService;

        public AddWalletHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public Task<Wallet> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = request.walletToAdd;

            ValidateWallet(wallet);
            CheckIfWalletExists(wallet);

            var addedWallet = _walletService.AddWallet(request.walletToAdd);

            return Task.FromResult(addedWallet);
        }

        private void CheckIfWalletExists(Wallet wallet)
        {
            var existingWallet = _walletService.GetWalletByName(wallet.Name);

            if (existingWallet != null)
            {
                if (
                    existingWallet.Address.Value == wallet.Address.Value &&
                    existingWallet.Network == wallet.Network)
                {
                    throw new WalletAlreadyExistsException(wallet.Address.Value, WalletExistsType.Address);
                }
                if (existingWallet.Name == wallet.Name)
                {
                    throw new WalletAlreadyExistsException(wallet.Name, WalletExistsType.Name);
                }
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