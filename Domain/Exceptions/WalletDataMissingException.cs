using System;

namespace Domain.Exceptions
{
    public class InvalidWalletDataException : Exception
    {
        public InvalidWalletDataException() : base("Invalid wallet data.")
        {
        }

        public InvalidWalletDataException(string message) : base(message)
        {
        }
    }
}