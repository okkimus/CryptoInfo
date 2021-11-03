using System.Threading.Tasks;
using Domain;

namespace Application.ServiceAbstractions
{
    public interface IWalletService
    { 
        Task<Wallet> AddWalletAsync(Wallet walletToAdd);
        Wallet GetWalletByName(string walletName);
        Wallet GetWalletByAddress(string address);
        double GetWalletValue(Wallet wallet);
    }
}