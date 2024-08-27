using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPIWork.Models.Entity
{
    public class Transaction
    {
        // internal string TransactionType;

        [Key]
        public Guid TransactionID { get; set; }  // Primary key for the transaction

        public Guid ReferenceNumber { get; set; }  // Reference number for the transaction
        public decimal AmountToBeDeposited { get; set; }  // Amount to be deposited
        public Guid AccountID { get; set; }  // Foreign key to the Account
        [ForeignKey("AccountID")]
        public Account Account { get; set; }  // Navigation property to Account

        public DateTime TransactionDate { get; set; }  // Date of the transaction
        //public string TransactionType { get; set; } // Changed to public
        public Transaction() { }

    }
}
