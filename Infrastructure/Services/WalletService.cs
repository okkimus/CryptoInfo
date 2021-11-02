using System;
using System.Collections.Generic;
using System.Linq;
using Application.ServiceAbstractions;
using Domain;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class WalletService : IWalletService
    {
        private readonly Dictionary<string, Wallet> _wallets = new Dictionary<string, Wallet>();

        public Wallet AddWallet(Wallet walletToAdd)
        {
            if (_wallets.ContainsKey(walletToAdd.Name))
            {
                throw new WalletNameExistsException();
            }

            _wallets.Add(walletToAdd.Name, walletToAdd);
            
            return walletToAdd;
        }

        public Wallet GetWalletByName(string walletName)
        {
            Wallet wallet;
            if (_wallets.TryGetValue(walletName, out wallet))
            {
                return wallet;
            } 
            else
            {
                throw new WalletNotFound($"Wallet with given name {walletName} doesn't exist.");
            }
        }

        public Wallet GetWalletByAddress(string address)
        {
            try
            {
                var wallet = _wallets.Values
                    .First(w => w.Address.Value == address);

                return wallet;
            }
            catch (InvalidOperationException e)
            {
                throw new WalletNotFound($"Wallet with given address {address} doesn't exist.");
            }
        }

        public double GetWalletValue(Wallet wallet)
        {
            double totalValue = 0;

            wallet.Balances.ForEach(bal =>
            {
                var tokenPrice = 12; // TODO: Get token price from Coingecko API?
                totalValue += bal.Amount * tokenPrice;
            });

            return totalValue;
        }
    }
}