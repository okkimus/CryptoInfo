﻿using Domain;
using MediatR;

namespace Application.Wallets.Commands.AddWallet
{
    public class AddWalletCommand : IRequest<Wallet>
    {
        public Wallet walletToAdd { get; set; }
    }
}