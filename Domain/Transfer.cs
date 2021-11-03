using System;

namespace Domain
{
    public class Transfer
    {
        public string Hash { get; }
        public DateTime DateTime { get; }
        public string From { get; }
        public string To { get; }
        public double Value { get; }
        public string ContractAddress { get; }
        public string TokenName { get; }
        public string TokenSymbol { get; }

        public Transfer(string hash, DateTime dateTime, string from, string to, double value, string contractAddress, string tokenName, string tokenSymbol)
        {
            Hash = hash;
            DateTime = dateTime;
            From = from;
            To = to;
            Value = value;
            ContractAddress = contractAddress;
            TokenName = tokenName;
            TokenSymbol = tokenSymbol;
        }
    }
}