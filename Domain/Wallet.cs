using System.Collections.Generic;

namespace Domain
{
    public class Wallet
    {
        public List<Balance> Balances { get; }
        public List<Transaction> Transactions { get; }
        public Network Network { get; }
        public Address Address { get; }
        public string Name { get; set; }

        public Wallet(Network network, Address address, string name, List<Balance> balances, List<Transaction> transactions)
        {
            Network = network;
            Address = address;
            Name = name;
            Balances = balances;
            Transactions = transactions;
        }
        
        public Wallet(Network network, Address address, string name)
        {
            Network = network;
            Address = address;
            Name = name;
            Balances = new List<Balance>();
            Transactions = new List<Transaction>();
        }
    }
}