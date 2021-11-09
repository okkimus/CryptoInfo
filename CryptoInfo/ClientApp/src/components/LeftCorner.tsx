import React from 'react';
import { useState } from "react";
import {Button, Stack} from "@mui/material";
import Wallets from "./Wallets";
import WalletForm from "./WalletForm";

function LeftCorner() {
    const [isAddMode, setIsAddMode] = useState<boolean>(false);

    const changeAddMode = () => {
        setIsAddMode(!isAddMode);
    }
    
    return (
        <Stack alignItems={"flex-start"}>
            {isAddMode?
                <WalletForm /> :
                <Wallets />
            }
            <Button variant="outlined" onClick={changeAddMode}>{isAddMode ? "Return" : "Add wallet"}</Button>
        </Stack>
    );
}

export default LeftCorner;
