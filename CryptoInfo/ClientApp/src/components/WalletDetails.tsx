import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Wallet from "../types/Wallet";

function WalletDetails() {
    const [wallet, setWallet] = useState<Wallet>();
    const [walletName, setWalletName] = useState<string>("");
    const [walletNames, setWalletNames] = useState<Array<string>>([]);
    
    const fetchWallet = () => {
        axios.get(`/wallet/name/${walletName}`)
            .then(data => handleWalletData(data.data));
    };
    
    const handleWalletData = (wallet: Wallet) => {
        console.log(wallet)
        setWallet(wallet);
    }

    // TODO: Store the wallets in global state so it can be shared between components easier.
    const getWallets = () => {
        axios.get("/wallet")
            .then(data => handleWalletsData(data.data));
    };

    const handleWalletsData = (wallets: Array<Wallet>) => {
        console.log(wallets);
        if (wallets.length > 0) {
            setWalletNames(wallets.map(w => w.name));
        }
    }

    return (
        <div className="WalletDetails">
            <label>Address</label>
            <select name="names" id="names" onChange={event => setWalletName(event.target.value)}>
                {walletNames.map(name => {
                        return <option value={name}>{name}</option>})}
            </select>

            <button onClick={fetchWallet}>
                Load wallet details
            </button>
            <button onClick={getWallets}>
                Get available wallets
            </button>
        </div>
    );
}

export default WalletDetails;
