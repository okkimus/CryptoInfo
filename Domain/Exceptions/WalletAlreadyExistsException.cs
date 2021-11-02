using System;

namespace Domain.Exceptions
{
    public class WalletAlreadyExistsException : Exception
    {
        public WalletAlreadyExistsException(string value, WalletExistsType type) : base(CreateExceptionMethod(value, type))
        {
        }

        private static string CreateExceptionMethod(string value, WalletExistsType type)
        {
            if (type == WalletExistsType.Name)
            {
                return $"Wallet with name {value} already exists.";
            }
            else if (type == WalletExistsType.Address)
            {
                return $"Wallet with address {value} already exists.";
            }
            else
            {
                return $"Wallet already exists.";
            }
        }
    }

    public enum WalletExistsType
    {
        NotSet,
        Name,
        Address
    }
}