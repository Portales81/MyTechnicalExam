using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Web.Models
{
    public class TransactionsViewModel
    {
        public string TrnsactionId { get; set; }      
        
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
       
        public string TransactionDate { get; set; }
       
        public string Status { get; set; }
    }
}
