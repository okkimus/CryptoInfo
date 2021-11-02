import React from 'react';
import { useState } from "react";
import axios from 'axios';

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
        <div className="FileUpload">
            <label>Address</label>
            <input type="text" onChange={event => setAddress(event.target.value)} />
            <label>Name</label>
            <input type="text" onChange={event => setName(event.target.value)} />

            <button onClick={createWallet}>
                Create wallet
            </button>
        </div>
    );
}

export default WalletForm;
