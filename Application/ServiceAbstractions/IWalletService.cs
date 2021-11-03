using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.ServiceAbstractions
{
    public interface IWalletService
    { 
        Task<Wallet> AddWalletAsync(Wallet walletToAdd);
        Task<List<Wallet>> GetWalletsAsync();
        Wallet GetWalletByName(string walletName);
        Wallet GetWalletByAddress(string address);
        double GetWalletValue(Wallet wallet);
    }
}