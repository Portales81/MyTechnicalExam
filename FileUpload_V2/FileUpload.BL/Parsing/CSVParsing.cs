using CsvHelper;
using FileUpload.BL.Model;
//using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.BusinessLogic
{
    public class CSVParsing
    {
        public IEnumerable<Transactions> extractCSV(string file)
        {
            List<Transactions> transactionList = new List<Transactions>();

            using (TextFieldParser csvReader = new TextFieldParser(file))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                var transactions = new Transactions();


                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();
                    //transactions.Id ++;
                    var transaction = new Transactions();
                    transaction.TrnsactionId = fields[0];
                    //transaction.Amount = Convert.ToDecimal(fields[1]);
                    transaction.CurrencyCode = fields[2];
                    string[] dateString = fields[3].Split('/');
                    DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
                    
                    //enter_date.ToString("dd/MM/yyyy HH:mm:ss");
                    //transactions.TransactionDate = Convert.ToDateTime(fields[3]);
                    transaction.TransactionDate = enter_date;
                    transaction.Status = fields[4];
                    transactionList.Add(transaction);
                }
            }                        
            return transactionList.ToList();

        }
    }

       
}
