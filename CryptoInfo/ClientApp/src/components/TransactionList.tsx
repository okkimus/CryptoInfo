import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Transaction from "../types/Transaction";

function TransactionList() {
    const [transactions, setTransactions] = useState<Array<Transaction>>([]);

    const getTransactions = () => {
        axios.get("/transaction")
            .then(data => handleTransactionData(data.data));
    };

    const handleTransactionData = (txs: Array<Transaction>) => {
        console.log(txs);
        if (txs.length > 0) {
            setTransactions(txs);
        }
    }

    return (
        <div className="Wallets">
            <table>
                <tr>
                    <th>Hash</th>
                    <th>From</th>
                    <th>To</th>
                </tr>
                {transactions.map(function(tx) {
                    return (<tr key={tx.hash}>
                                <td>{tx.hash}</td>
                                <td>{tx.from}</td>
                                <td>{tx.to}</td>
                            </tr>)
                })}
            </table>

            <button onClick={getTransactions}>
                Fetch transactions
            </button>
        </div>
    );
}

export default TransactionList;
