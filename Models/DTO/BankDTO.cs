
using System.ComponentModel.DataAnnotations;
using UPIWork.Models.Entity;


namespace UPIWork.Models.DTO
{
    public class BankDTO : FinancialInstitution
    {
        [Required]
        public string Name { get; set; }  // BankName
        public string? Address { get; set; }  // Address of bank
        public string? Country { get; set; }   // Country of Bank
        [Required]
        public bool IsCertified { get; set; } // whether the bank is certified
        public string BankType { get; set; }  // type of bank
        [Required]
        public DateTime CertificationExpiryDate { get; set; }   // 
        public bool IsNationalized { get; set; }  // whether the bank is national or not?

    }
}