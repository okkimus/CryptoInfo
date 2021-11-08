using System;
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

        private bool _txsLeftToRead = false;
        private bool _transfersLeftToRead = false;
        
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

            _txsLeftToRead = txsEnumerator.MoveNext();
            _transfersLeftToRead = transfersEnumerator.MoveNext();

            try
            {
                while (_txsLeftToRead || _transfersLeftToRead)
                {
                    var bothAvailable = _txsLeftToRead && _transfersLeftToRead;

                    if (bothAvailable)
                    {
                        var result = CreateTxWhenBothAvailable(txsEnumerator, transfersEnumerator);
                        resultTransactions.AddRange(result);
                    }
                    else if (_txsLeftToRead)
                    {
                        var currentTx = txsEnumerator.Current;
                        resultTransactions.Add(MapTransaction(currentTx));
                        _txsLeftToRead = txsEnumerator.MoveNext();
                    }
                    else
                    {
                        resultTransactions.Add(CreateExternalTransaction(transfersEnumerator));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            EndImport();
            
            return resultTransactions;
        }

        private List<Transaction> CreateTxWhenBothAvailable(IEnumerator<FtmScanTx> txs, IEnumerator<FtmScanTransfer> transfers)
        {
            var currentTx = txs.Current;
            var currentTransfer = transfers.Current;
            var result = new List<Transaction>();
            
            if (currentTransfer.DateTime == currentTx.DateTime)
            {
                if (currentTransfer.Txhash == currentTx.Txhash)
                {
                    result.Add(CreateTransactionWithTransfers(currentTx, transfers));
                    _txsLeftToRead = txs.MoveNext();
                }
                else
                {
                    // TODO: There possibility for collision with external TX and wallets own TX which could lead into
                    // situation where transfers aren't read correctly to the tx. Need to add tests for this case and
                    // update the code afterwards! (Happens when the two or more tx related to address are added to same
                    // block on chain.)
                    
                    // Create own tx and create external tx.
                    result.Add(MapTransaction(currentTx));
                    _txsLeftToRead = txs.MoveNext();
                    result.Add(CreateExternalTransaction(transfers));
                }
            }
            else if (currentTransfer.DateTime > currentTx.DateTime)
            {
                result.Add(MapTransaction(currentTx));
                _txsLeftToRead = txs.MoveNext();
            }
            else if (currentTransfer.DateTime < currentTx.DateTime)
            {
                result.Add(CreateExternalTransaction(transfers));
            }

            return result;
        }

        private Transaction CreateTransactionWithTransfers(FtmScanTx tx, IEnumerator<FtmScanTransfer> transfers)
        {
            var transaction = MapTransaction(tx);

            return MapTransfersToTx(transaction, transfers);
        }
        
        private Transaction CreateExternalTransaction(IEnumerator<FtmScanTransfer> transfers)
        {
            var currentTransfer = GetCurrentTransfer(transfers);
            
            var externalTx = new Transaction(currentTransfer.Txhash, currentTransfer.DateTime, 
                string.Empty, string.Empty, 0, 0, true);

            return MapTransfersToTx(externalTx, transfers);
        }

        private Transaction MapTransfersToTx(Transaction tx, IEnumerator<FtmScanTransfer> transfers)
        {
            while (_transfersLeftToRead && transfers.Current?.Txhash == tx.Hash)
            {
                var currentTransfer = transfers.Current;
                tx.AddTransfer(MapTransfer(currentTransfer));
                _transfersLeftToRead = transfers.MoveNext();
            }

            return tx;
        }

        private FtmScanTransfer GetCurrentTransfer(IEnumerator<FtmScanTransfer> transfers)
        {
            var currentTransfer = transfers.Current;

            if (currentTransfer == null)
            {
                throw new ArgumentNullException("No Transfer objects left in enumerator.");
            }

            return currentTransfer;
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
        public double Value_IN { get; set; }
        [Index(8)]
        public double Value_OUT { get; set; }
        [Index(9)]
        public double CurrentValue { get; set; }
        [Index(10)]
        public double TxnFeeFtm { get; set; }
        [Index(11)]
        public double TxnFeeUsd { get; set; }
        [Index(12)]
        public double? HistoricalPricePerFtm { get; set; }
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