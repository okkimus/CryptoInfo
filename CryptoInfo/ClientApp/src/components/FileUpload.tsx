import React from 'react';
import { useState } from "react";
import axios from 'axios';

function FileUpload() {
    const [txFile, setTxFile] = useState<any>(null);
    const [transferFile, setTransferFile] = useState<any>(null);
    
    const onTxFileChange = (event: any) => {
        setTxFile(event.target.files[0]);
    };

    const onTrasnferFileChange = (event: any) => {
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

        // Details of the uploaded file
        console.log(txFile);
        console.log(transferFile);

        // Request made to the backend api
        // Send formData object
        axios.post("/import/transactions", formData);
    };

    const test = () => {
        const text = axios.get("/import/hello");
        console.log(text)
    };
    
    return (
        <div className="FileUpload">
            <input type="file" onChange={onTxFileChange} />
            <input type="file" onChange={onTrasnferFileChange} />
            <button onClick={onFileUpload}>
                Upload!
            </button>
            
            <button onClick={test}>Test</button>
        </div>
    );
}

export default FileUpload;
