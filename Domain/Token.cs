namespace Domain
{
    public class Token
    {
        public string Name { get; set; }
        public string ContractAddress { get; set; }
        public string Ticker { get; set; }

        public Token(string name, string contractAddress, string ticker)
        {
            Name = name;
            ContractAddress = contractAddress;
            Ticker = ticker;
        }
    }
}