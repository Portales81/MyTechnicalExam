﻿using FileUpload.BL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUpload.Data.Repository.Interface
{
    public interface ITransactionsRepository
    {
        void SaveTransaction(Transactions transaction);
        IEnumerable<Transactions> GetAllTransactions();
        Transactions GetTransaction(int id);

    }
}