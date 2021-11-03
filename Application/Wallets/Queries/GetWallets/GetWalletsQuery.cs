using System.Collections.Generic;
using Domain;
using MediatR;

namespace Application.Wallets.Queries.GetWallets
{
    public class GetWalletsQuery : IRequest<List<Wallet>> {}
}