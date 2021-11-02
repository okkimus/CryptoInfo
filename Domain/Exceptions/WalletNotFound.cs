using System;

namespace Domain.Exceptions
{
    public class WalletNotFound : Exception
    {
        public WalletNotFound(): base("Wallet was not found.")
        {
        }
        
        public WalletNotFound(string message): base(message)
        {
        }
    }
}