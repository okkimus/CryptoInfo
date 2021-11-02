using System;

namespace Domain.Exceptions
{
    public class NotEnoughBalanceException : Exception
    {
        public NotEnoughBalanceException(): base("Balance less than wanted to substract.")
        {
        }
    }
}