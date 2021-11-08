using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceAbstractions;
using Domain;

namespace Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public Task<List<Transaction>> GetTransactionsAsync()
        {
            return Task.FromResult(_transactions);
        }

        public Task<List<Transaction>> GetTransactionsByWalletAddressAsync(string address)
        {
            var txs = _transactions.FindAll(tx => 
                tx.From.Equals(address, StringComparison.OrdinalIgnoreCase) || 
                tx.Transfers.Any(transfer => transfer.To.Equals(address, StringComparison.OrdinalIgnoreCase)));

            return Task.FromResult(txs);
        }

        public Task<Transaction> GetTransactionByHashAsync(string hash)
        {
            throw new System.NotImplementedException();
        }

        public Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public Task<List<Transaction>> AddTransactionsAsync(List<Transaction> transactions)
        {
            _transactions.AddRange(transactions);
            return Task.FromResult(transactions);
        }
    }
}