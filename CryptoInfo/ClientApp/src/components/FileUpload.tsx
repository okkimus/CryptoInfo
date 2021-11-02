import React from 'react';
import { useState } from "react";
import axios from 'axios';

function FileUpload() {
    const [file, setFile] = useState<any>(null);
    
    const onFileChange = (event: any) => {
        setFile(event.target.files[0]);
    };
    
    const onFileUpload = () => {
        // Create an object of formData
        const formData = new FormData();

        // Update the formData object
        formData.append(
            "file",
            file,
            file.name
        );

        // Details of the uploaded file
        console.log(file);

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
            <input type="file" onChange={onFileChange} />
            <button onClick={onFileUpload}>
                Upload!
            </button>
            
            <button onClick={test}>Test</button>
        </div>
    );
}

export default FileUpload;
