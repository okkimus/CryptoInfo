﻿import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Wallet from "../types/Wallet";

function Wallets() {
    const [wallets, setWallets] = useState<Array<Wallet>>([]);

    const getWallets = () => {
        axios.get("/wallet")
            .then(data => handleWalletData(data.data));
    };
    
    const handleWalletData = (wallets: Array<Wallet>) => {
        console.log(wallets);
        if (wallets.length > 0) {
            setWallets(wallets);
        }
    }

    return (
        <div className="Wallets">
            <ul>
                {wallets.map(function(wallet) {
                    return <li key={wallet.address.value}>{wallet.address.value} + {wallet.name}</li>;
                })}
            </ul>

            <button onClick={getWallets}>
                Fetch wallets
            </button>
        </div>
    );
}

export default Wallets;
