using System;

namespace Domain.Exceptions
{
    public class WalletNameExistsException : Exception
    {
        public WalletNameExistsException() : base("Wallet name already exists.")
        {
        }
    }
}