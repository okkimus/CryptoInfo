import React from 'react';
import { useState } from "react";
import axios from 'axios';
import Transaction from "../types/Transaction";
import {Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";

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
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Hashs</TableCell>
                            <TableCell>Timestamp</TableCell>
                            <TableCell>From</TableCell>
                            <TableCell>To</TableCell>
                            <TableCell>Value</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {transactions.map((tx) => (
                            <TableRow
                                key={tx.hash}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">{tx.hash}</TableCell>
                                <TableCell>{tx.timestamp}</TableCell>
                                <TableCell>{tx.from}</TableCell>
                                <TableCell>{tx.to}</TableCell>
                                <TableCell>{tx.valueOut}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <button onClick={getTransactions}>
                Fetch transactions
            </button>
        </div>
    );
}

export default TransactionList;
