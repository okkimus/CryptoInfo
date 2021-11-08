import React from 'react';
import './App.css';
import FileUpload from "./components/FileUpload";
import WalletForm from "./components/WalletForm";
import Wallets from "./components/Wallets";
import TransactionList from "./components/TransactionList";
import WalletDetails from "./components/WalletDetails";

function App() {
  return (
    <div className="App">
      <header className="App-header">
          <Wallets />
          <FileUpload />
          <WalletForm />
          <TransactionList />
          <WalletDetails />
      </header>
    </div>
  );
}

export default App;
