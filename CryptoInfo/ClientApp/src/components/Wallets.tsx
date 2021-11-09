import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Wallet from "../types/Wallet";
import {Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField} from "@mui/material";

function Wallets() {
    const [wallets, setWallets] = useState<Array<Wallet>>([]);
    const [selectedWallet, setSelectedWallet] = useState<Wallet>();
    const [selectedWalletAddress, setSelectedWalletAddress] = useState<string>("0");

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
    
    const handleSelectChange = (event: SelectChangeEvent) => {
        const address = event.target.value;
        const wallet = wallets.find(w => w.address.value.toLowerCase() == address.toLowerCase());
        if (wallet != null) {
            setSelectedWallet(wallet);
            setSelectedWalletAddress(address);
        }
    }

    return (
        <Stack alignItems="flex-start" spacing={2} className="Wallets">
            <FormControl fullWidth>
                <InputLabel id="wallet-label">Wallet</InputLabel>
                <Select
                    labelId="wallet-label"
                    value={selectedWalletAddress}
                    label="Wallet"
                    onChange={handleSelectChange}
                >
                    <MenuItem value={"0"}>- Select wallet -</MenuItem>
                    {wallets.map(function(wallet) {
                        return <MenuItem key={wallet.address.value} value={wallet.address.value}>{wallet.name}</MenuItem>;
                    })}
                </Select>
            </FormControl>
            
            <Button variant="contained" onClick={getWallets}>
                Fetch wallets
            </Button>
        </Stack>
    );
}

export default Wallets;
