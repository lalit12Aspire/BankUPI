namespace UPIWork.Models.DTO{
    public class InsuranceCompanyDTO{
        public string Name{get;set;}
        public string? Address{get;set;}
        public string? Country{get;set;}
        public bool IsCertified{get;set;}
        public string IsReinsurer{get;set;}
        public DateTime CertificatonDate{get;
        set;}
        public int TotalPremiums{get;set;}
        
        public DateTime CertificationExpiryDate{get;set;}
    }
}