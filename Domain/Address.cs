namespace Domain
{
    public class Address
    {
        public AddressType Type { get; }
        public string Value { get;  }

        public Address(string value, AddressType type)
        {
            Value = value;
            Type = type;
        }
    }
}