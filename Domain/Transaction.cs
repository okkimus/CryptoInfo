using System;
using System.Collections.Generic;

namespace Domain
{
    public class Transaction
    {
        private string _hash { get; }
        private DateTime _timestamp { get; }
        private Address _invoker { get; }
        private Address _contract { get; }
        private List<Transfer> _transfers { get; }
    }
}