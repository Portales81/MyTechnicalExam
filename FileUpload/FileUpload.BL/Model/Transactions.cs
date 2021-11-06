using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.BL.Model
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TrnsactionId { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string  Status { get; set; }
        
    }
}
