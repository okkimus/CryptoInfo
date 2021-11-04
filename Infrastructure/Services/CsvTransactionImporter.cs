﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Application.ServiceAbstractions;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Domain;

namespace Infrastructure.Services
{
    public class CsvTransactionImporter : ITransactionImporter
    {
        private CsvImporterOptions _options;
        private List<CsvReader> _csvReaders = new List<CsvReader>();
        private List<StreamReader> _streamReaders = new List<StreamReader>();
        
        public List<Transaction> ReadTransactions(ITransactionImporterOptions options)
        {
            _options = options as CsvImporterOptions;
            var resultTransactions = new List<Transaction>();
            
            var txs = ReadRecords<FtmScanTx>(_options.TxCsv);
            var transfers = ReadRecords<FtmScanTransfer>(_options.TransferCsv);
            
            // We'll deal with enumerators here to no eagerly load all txs to memory
            var txsEnumerator = txs.GetEnumerator();
            var transfersEnumerator = transfers.GetEnumerator();
            
            // There's 3 different situations.
            // 1. The tx is only in TxCsv (or in other words, there wasn't any ERC-20 transfers in that tx),
            //    i.e. approving token for spending)
            // 2. The tx is in TxCsv and TransferCsv, i.e. when swapping tokens in a DEX)
            // 3. The tx in only in TransferCsv (the tx was initiated from another wallet / contract),
            //    i.e. when someone sends you ERC-20 tokens or liquidation happens.
            
            // Handling of situations:
            // 1. Create a Transaction object without any transfers.
            // 2. Create a Transaction object with all the transfers associated with it.
            // 3. Create a Transaction object with all the transfers, but flag the transaction as external.
            
            // Logic:
            // Read lines from each file at the same time. Compare the timestamps:
            // - If the timestamp are same, check if tx hashes match and if so group them together. If not,
            //   create separate txs
            // - Select earlier one and create tx from that. Read next line after the earliest tx and repeat.

            transfersEnumerator.MoveNext();
            var currentTransfer = transfersEnumerator.Current;

            while (txsEnumerator.MoveNext())
            {
                var tx = MapTransaction(txsEnumerator.Current);

                while (currentTransfer.Txhash == tx.Hash)
                {
                    var transfer = MapTransfer(currentTransfer);       
                    tx.AddTransfer(transfer);

                    transfersEnumerator.MoveNext();
                }
                
                resultTransactions.Add(tx);
            }
            
            EndImport();
            
            return resultTransactions;
        }

        public IEnumerable<T> ReadRecords<T>(MemoryStream stream)
        {
            var streamReader = new StreamReader(stream);
            var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            // Store the readers so we can dispose them later.
            _csvReaders.Add(reader);
            _streamReaders.Add(streamReader);
            
            var records = reader.GetRecords<T>();
            return records;
        }

        public Transaction MapTransaction(FtmScanTx ftmScanTx)
        {
            return new Transaction
            (
                ftmScanTx.Txhash, ftmScanTx.DateTime, 
                ftmScanTx.From, ftmScanTx.To,
                ftmScanTx.Value_IN, ftmScanTx.Value_OUT
            );
        }

        public Transfer MapTransfer(FtmScanTransfer ftmScanTransfer)
        {
            return new Transfer
            (
                ftmScanTransfer.Txhash, ftmScanTransfer.DateTime, 
                ftmScanTransfer.From, ftmScanTransfer.To, 
                ftmScanTransfer.Value, ftmScanTransfer.ContractAddress, 
                ftmScanTransfer.TokenName, ftmScanTransfer.TokenSymbol
            );
        }

        
        private void EndImport()
        {
            foreach (var reader in _csvReaders)
            {
                reader.Dispose();
            }
            foreach (var reader in _streamReaders)
            {
                reader.Dispose();
            }
            
            _options.TransferCsv.Dispose();
            _options.TxCsv.Dispose();
        }
    }

    public class FtmScanTx
    {
        [Index(0)]
        public string Txhash { get; set; }
        [Index(1)]
        public int Blockno { get; set; }
        [Index(2)]
        public int UnixTimestamp { get; set; }
        [Index(3)]
        public DateTime DateTime { get; set; }
        [Index(4)]
        public string From { get; set; }
        [Index(5)]
        public string To { get; set; }
        [Index(6)]
        public string ContractAddress { get; set; }
        [Index(7)]
        public int Value_IN { get; set; }
        [Index(8)]
        public int Value_OUT { get; set; }
        [Index(9)]
        public int CurrentValue { get; set; }
        [Index(10)]
        public double TxnFeeFtm { get; set; }
        [Index(11)]
        public double TxnFeeUsd { get; set; }
        [Index(12)]
        public double HistoricalPricePerFTM { get; set; }
        [Index(13)]
        public string Status { get; set; }
        [Index(14)]
        public string ErrCode { get; set; }
        [Index(15)]
        public string Method { get; set; }
    }
    
    public class FtmScanTransfer
    {
        public string Txhash { get; set; }
        public int UnixTimestamp { get; set; }
        public DateTime DateTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Value { get; set; }
        public string ContractAddress { get; set; }
        public string TokenName { get; set; }
        public string TokenSymbol { get; set; }
    }
    
    public class CsvImporterOptions : ITransactionImporterOptions {
        public MemoryStream TxCsv { get; set; }
        public MemoryStream TransferCsv { get; set; }
    }
}