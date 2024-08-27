namespace UPIWork.Models.DTO
{
    public class CreditUnionDTO
    {
        public Guid AccountID { get; set; }
        public decimal Amount { get; set; }
        public string Name
        {
            get;
            set;
        }
        public string Address { get; set; }
        public string Country
        {
            get; set;
        }

        public bool IsCertified
        {
            get;
            set;
        }
        public DateTime CertificationExpiryDate { get; set; }
        public decimal TotalDeposits
        {
            get; set;
        }
        public bool IsMemberOwned { get; set; }
        public string CreditUnionType
        {
            get; set;
        }

    }
}