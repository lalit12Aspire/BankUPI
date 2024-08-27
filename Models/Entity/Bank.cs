using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UPIWork.Models.Entity;
public class Bank : FinancialInstitution
{
    [Key]
    public Guid BankId { get; set; }
    [Required]
    public string BankName { get; set; }
    public bool NationalBank { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    [Required]
    public bool IsCertified { get; set; }

    public string BankType { get; set; }

    [Required]
    public DateTime CertificationExpiryDate { get; set; }
    // [ForeignKey("AccountId")]
    // public Guid AccountId { get; set; }

    // public Account Account { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    // public string BankName{get; set;}


}

