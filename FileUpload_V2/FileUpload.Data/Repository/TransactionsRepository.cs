using FileUpload.BL.Model;
using FileUpload.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileUpload.Data.Repository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private ApplicationDBContext _context;
        private DbSet<Transactions> transactionEntity;

        public TransactionsRepository(ApplicationDBContext context)
        {
            _context = context;
            transactionEntity = context.Set<Transactions>();

        }
        public IEnumerable<Transactions> GetAllTransactions()
        {
            return transactionEntity.AsEnumerable();
        }

        public Transactions GetTransaction(int id)
        {
            return transactionEntity.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<Transactions> GetTransactionByCurrency(string currencyCode)
        {
           return _context.Transactions.Where(x => x.CurrencyCode == currencyCode.ToLower().TrimEnd()).ToList();
        }

        public IEnumerable<Transactions> GetTransactionByDateRange(DateTime startdate, DateTime endDate)
        {
            return _context.Transactions.Where(x => 
            x.TransactionDate <= startdate
            && x.TransactionDate >= endDate).ToList();
        }

        public IEnumerable<Transactions> GetTransactionByStatus(string status)
        {
            return _context.Transactions.Where(x => x.Status == status.ToLower().TrimEnd()).ToList();
        }

        public void SaveTransaction(Transactions transaction)
        {
            _context.Entry(transaction).State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
