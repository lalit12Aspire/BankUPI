namespace UPIWork.Models.Entity
{
    public abstract class FinancialInstitution
    {

        public string? BankName { get; set; }
        public string? BankType { get; set; }
        private string? BankID { get; set; }
        public bool IsCertified { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; } // City Of the Bank/Financial_Institution
        public string? Address { get; set; }
    }
}
