using Domain.Exceptions;

namespace Domain
{
    public class Balance
    {
        public Token Token { get; }
        public double Amount { get; set; }

        public Balance(Token token, double amount)
        {
            Token = token;
            Amount = amount;
        }
        
        public Balance(Token token)
        {
            Token = token;
        }

        public double Add(double amount)
        {
            Amount += amount;
            return Amount;
        }
        
        public double Substract(double amount)
        {
            if (Amount < amount)
            {
                throw new NotEnoughBalanceException();
            }

            Amount -= amount;

            return Amount;
        }
    }
}