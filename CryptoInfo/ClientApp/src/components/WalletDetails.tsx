import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Wallet from "../types/Wallet";
import Button from '@mui/material/Button';
import {FormControl, InputLabel, MenuItem, Select, Stack} from "@mui/material";

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
        <Stack alignItems="flex-start" className="WalletDetails">
            <FormControl fullWidth>
                <InputLabel id="wallet-label">Wallet</InputLabel>
                <Select
                    labelId="wallet-label"
                    value={walletName}
                    label="Wallet"
                    onChange={event => setWalletName(event.target.value)}>
                    <MenuItem value={"0"}>- Select wallet -</MenuItem>
                    {walletNames.map(name => {
                        return <MenuItem key={name} value={name}>{name}</MenuItem>})}
                </Select>
            </FormControl>

            <Button onClick={fetchWallet}>
                Load wallet details
            </Button>
            <Button onClick={getWallets}>
                Get available wallets
            </Button>
        </Stack>
    );
}

export default WalletDetails;
