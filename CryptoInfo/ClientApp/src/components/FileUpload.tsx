import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Button from '@mui/material/Button';
import { Stack } from '@mui/material';

function FileUpload() {
    const [txFile, setTxFile] = useState<any>(null);
    const [transferFile, setTransferFile] = useState<any>(null);
    
    const onTxFileChange = (event: any) => {
        setTxFile(event.target.files[0]);
    };

    const onTransferFileChange = (event: any) => {
        setTransferFile(event.target.files[0]);
    };
    
    const onFileUpload = () => {
        const formData = new FormData();

        formData.append(
            "txFile",
            txFile,
            txFile.name
        );
        
        formData.append(
            "transferFile",
            transferFile,
            transferFile.name
        );

        axios.post("/import/transactions", formData);
    };
    
    return (
        <Stack className="FileUpload">
            <Button
                variant="contained"
                component="label">
                Select TX CSV
                <input
                    type="file"
                    hidden
                    onChange={onTxFileChange}
                />
            </Button>
            <div>Selected file: {txFile?.name}</div>
            
            <Button
                variant="contained"
                component="label">
                Select transfer CSV
                <input
                    type="file"
                    hidden
                    onChange={onTransferFileChange}
                />
            </Button>
            <div>Selected file: {transferFile?.name}</div>

            <Button variant="contained" onClick={onFileUpload}>
                Upload!
            </Button>
        </Stack>
    );
}

export default FileUpload;
