import React from 'react';
import { useState } from "react";
import {Button, Stack} from "@mui/material";
import FileUpload from "./FileUpload";
import WalletDetails from "./WalletDetails";

function LeftCorner() {
    const [isImportMode, setIsImportMode] = useState<boolean>(false);

    const changeAddMode = () => {
        setIsImportMode(!isImportMode);
    }

    return (
        <Stack alignItems={"flex-start"}>
            {isImportMode ?
                <FileUpload /> :
                <WalletDetails />
            }
            <Button variant={"outlined"} onClick={changeAddMode}>{isImportMode ? "Return" : "Import TXs"}</Button>
        </Stack>
    );
}

export default LeftCorner;
