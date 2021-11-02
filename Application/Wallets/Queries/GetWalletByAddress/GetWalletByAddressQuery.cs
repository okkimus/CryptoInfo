using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWalletByAddress
{
    public class GetWalletByAddressQuery : IRequest<Wallet>
    {
        public string Address { get; set; }
    }
}