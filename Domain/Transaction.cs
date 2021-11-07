using System;
using System.Collections.Generic;

namespace Domain
{
    public class Transaction
    {
        public string Hash { get; }
        public DateTime Timestamp { get; }
        public string From { get; }
        public string To { get; }
        public double ValueIn { get; set; }
        public double ValueOut { get; set; }
        public List<Transfer> Transfers { get; }
        public bool External { get; set; }
        

        public Transaction(string hash, DateTime timestamp, string from, string to, double valueIn, double valueOut)
        {
            Hash = hash;
            Timestamp = timestamp;
            From = from;
            To = to;
            ValueIn = valueIn;
            ValueOut = valueOut;
            Transfers = new List<Transfer>();
        }
        
        public Transaction(string hash, DateTime timestamp, string from, string to, double valueIn, double valueOut, bool external)
        {
            Hash = hash;
            Timestamp = timestamp;
            From = from;
            To = to;
            ValueIn = valueIn;
            ValueOut = valueOut;
            Transfers = new List<Transfer>();
            External = external;
        }

        public Transaction(string hash, DateTime timestamp, string from, string to, double valueIn, double valueOut, List<Transfer> transfers)
        {
            Hash = hash;
            Timestamp = timestamp;
            From = from;
            To = to;
            ValueIn = valueIn;
            ValueOut = valueOut;
            Transfers = transfers;
        }

        public void AddTransfer(Transfer t)
        {
            Transfers.Add(t);
        }
    }
}