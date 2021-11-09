import React from 'react';
import { useState } from "react";
import axios from 'axios';
import {Button, Stack, TextField} from "@mui/material";

function WalletForm() {
    const [name, setName] = useState<string>("");
    const [address, setAddress] = useState<string>("");

    const createWallet = () => {
        const body = {
            name: name,
            address: address
        }
        
        axios.post("/wallet", body);
    };

    return (
        <Stack alignItems="flex-start" spacing={2} className="FileUpload">
            <TextField value={address} label="Address" variant="outlined" 
                       onChange={event => setAddress(event.target.value)} />
            <TextField value={name} label="Name" variant="outlined" 
                       onChange={event => setName(event.target.value)} />

            <Button disabled={name == "" || address == ""} variant="contained" onClick={createWallet}>
                Create wallet
            </Button>
        </Stack>
    );
}

export default WalletForm;
