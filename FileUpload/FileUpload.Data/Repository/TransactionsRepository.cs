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

        public void SaveTransaction(Transactions transaction)
        {
            _context.Entry(transaction).State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
