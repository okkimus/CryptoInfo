using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.ServiceAbstractions
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync();
        Task<List<Transaction>> GetTransactionsByWalletAddressAsync(string address);
        Task<Transaction> GetTransactionByHashAsync(string hash);
        Task<Transaction> AddTransactionAsync(Transaction transaction);

        Task<List<Transaction>> AddTransactionsAsync(List<Transaction> transactions);
        
    }
}