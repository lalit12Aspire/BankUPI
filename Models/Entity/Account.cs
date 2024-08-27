using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UPIWork.Models.Entity
{
    public class Account
    {
        [Key]

        public Guid AccountID { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string? CardNumber { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public string CVv { get; set; }
        public string UPIID { get; set; }

        // public string BankName { get; set; } // In which Bank is the account?
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }

        // public Guid CreditUnionId { get; set; }
        // public CreditUnion CreditUnion { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();


        public Account()
        {
            CardNumber = GenerateCardNumber();
            CVv = GenerateCVV();
            CardExpiryDate = DateTime.Now.AddYears(5);
        }

        private string GenerateAccountNumber()
        {
            // Generate a random 10-digit account number
            Random random = new Random();
            string accountNumber = random.Next(10000, 99999).ToString() + random.Next(10000, 99999).ToString();
            return accountNumber;
        }


        public string GenerateCardNumber()
        {
            // Generate a random 12-digit card number
            Random random = new Random();
            long minValue = 100000000000; // 12-digit minimum
            long maxValue = 999999999999; // 12-digit maximum

            long cardNumber = random.NextInt64(minValue, maxValue + 1);
            return cardNumber.ToString();
        }

        public string GenerateCVV()
        {
            Random random = new Random();
            return random.Next(100, 999).ToString();
        }



        public bool IsCardValid()
        {
            return DateTime.Now <= CardExpiryDate;
        }
    }
}
