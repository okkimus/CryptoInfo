using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Services;
using Xunit;

namespace Tests.UnitTests.CsvImport
{
    public class CsvTransactionImporterTests
    {
        private readonly CsvTransactionImporter _sut = new CsvTransactionImporter();

        [Fact]
        public void ReadTransactions_GivenTransactionFileWithATransaction_CreatesATransaction()
        {
            var options = new CsvImporterOptions
            {
                TxCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/TxFileWithATransaction.csv"),
                TransferCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/EmptyTransfersFile.csv")
            };
            var transactions = _sut.ReadTransactions(options);
            
            CloseStreams(options);
            
            Assert.Single(transactions);
        }
        
        [Fact]
        public void ReadTransactions_GivenTransactionFileWithTwoTransaction_CreatesTwoTransaction()
        {
            var options = new CsvImporterOptions
            {
                TxCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/TxFileWithTwoTransactions.csv"),
                TransferCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/EmptyTransfersFile.csv")
            };
            var transactions = _sut.ReadTransactions(options);
            
            CloseStreams(options);
            
            Assert.Equal(2, transactions.Count);
        }
        
        [Fact]
        public void ReadTransactions_GivenTransactionFileWithATransactionWithATransfer_CreatesATransactionWithATransfer()
        {
            var options = new CsvImporterOptions
            {
                TxCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/TxFileWithATransaction.csv"),
                TransferCsv = CreateMemoryStream("UnitTests/CsvImport/TestFiles/TransferFileWithATransfer.csv")
            };
            var transactions = _sut.ReadTransactions(options);
            
            CloseStreams(options);
            
            Assert.Single(transactions);
            Assert.Single(transactions.First().Transfers);
        }

        private MemoryStream CreateMemoryStream(string path)
        {
            var stream = new MemoryStream(File.ReadAllBytes(path));
            stream.Position = 0;

            return stream;
        }

        private void CloseStreams(CsvImporterOptions options)
        {
            options.TransferCsv.Dispose();
            options.TxCsv.Dispose();
        }
    }
}