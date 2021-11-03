import React from 'react';
import './App.css';
import FileUpload from "./components/FileUpload";
import WalletForm from "./components/WalletForm";
import Wallets from "./components/Wallets";


function App() {
  return (
    <div className="App">
      <header className="App-header">
          <Wallets />
          <FileUpload />
          <WalletForm />
      </header>
    </div>
  );
}

export default App;
