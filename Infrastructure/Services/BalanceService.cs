using System;
using System.Collections.Generic;
using System.Linq;
using Application.ServiceAbstractions;
using Domain;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class BalanceService : IBalanceService
    {
        public List<Balance> CalculateBalances(Wallet wallet)
        {
            if (wallet.Transactions == null || wallet.Transactions.Count == 0)
            {
                throw new WalletHasNoTransactionsException();
            }

            var balances = new Dictionary<string, Balance>();

            foreach (var tx in wallet.Transactions)
            {
                // TODO: Add the native token balances from Transaction object.
                
                foreach (var transfer in tx.Transfers)
                {
                    if (transfer.From.Equals(wallet.Address.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        var tokenContractAddress = transfer.ContractAddress;
                        var tokenAmount = transfer.Value;
                        Token token;
                        Balance balance = null;
                        if (balances.TryGetValue(tokenContractAddress, out balance))
                        {
                            balance.Amount -= tokenAmount;
                        }
                        else
                        {
                            token = new Token(transfer.TokenName, transfer.ContractAddress, transfer.TokenSymbol);
                            balance = new Balance(token, -tokenAmount);
                        }
                        balances[tokenContractAddress] = balance;
                    } 
                    else if (transfer.To.Equals(wallet.Address.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        var tokenContractAddress = transfer.ContractAddress;
                        var tokenAmount = transfer.Value;
                        Token token;
                        Balance balance = null;
                        if (balances.TryGetValue(tokenContractAddress, out balance))
                        {
                            balance.Amount += tokenAmount;
                        }
                        else
                        {
                            token = new Token(transfer.TokenName, transfer.ContractAddress, transfer.TokenSymbol);
                            balance = new Balance(token, tokenAmount);
                        }
                        balances[tokenContractAddress] = balance;
                    }
                }
            }

            var resultBalances = balances.Values.ToList();

            return resultBalances;
        }

        public List<Balance> CalculateBalances(List<Transaction> txs, string walletAddress)
        {
            throw new System.NotImplementedException();
        }
    }
}