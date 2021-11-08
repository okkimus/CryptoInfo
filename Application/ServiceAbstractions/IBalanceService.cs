using System.Collections.Generic;
using Domain;

namespace Application.ServiceAbstractions
{
    public interface IBalanceService
    {
        List<Balance> CalculateBalances(Wallet wallet);
        List<Balance> CalculateBalances(List<Transaction> txs, string walletAddress);
    }
}