using System;

namespace Domain.Exceptions
{
    public class WalletHasNoTransactionsException : Exception
    {
        public WalletHasNoTransactionsException() : base("Wallet has no transactions.") { }
    }
}