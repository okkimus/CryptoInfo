using Domain;

namespace Application.ServiceAbstractions
{
    public interface IWalletService
    { 
        Wallet AddWallet(Wallet walletToAdd);
        Wallet GetWalletByName(string walletName);
        Wallet GetWalletByAddress(string address);
        double GetWalletValue(Wallet wallet);
    }
}