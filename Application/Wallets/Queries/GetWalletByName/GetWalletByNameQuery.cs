using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWalletByName
{
    public class GetWalletByNameQuery : IRequest<Wallet>
    {
        public string Name { get; set; }
    }
}