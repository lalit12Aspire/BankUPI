namespace UPIWork.Models.DTO
{
    public class AccountDTO
    {
        //internal Guid accountId;

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string? CardNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string? CVv { get; set; }
        public DateTime CardExpiryDate { get; set; }
       // public Guid CustomerId { get; set; }

    }
}