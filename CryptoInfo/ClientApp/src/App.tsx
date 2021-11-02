import React from 'react';
import './App.css';
import FileUpload from "./components/FileUpload";
import WalletForm from "./components/WalletForm";


function App() {
  return (
    <div className="App">
      <header className="App-header">
          <FileUpload />
          <WalletForm />
      </header>
    </div>
  );
}

export default App;
