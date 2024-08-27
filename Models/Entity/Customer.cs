using System.ComponentModel.DataAnnotations;
using UPIWork.Models.Entity;
public class Customer
{
    [Key]
    public Guid CustomerId { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z\s]{1,100}$", ErrorMessage = "Name must only contain letters and spaces, and be up to 100 characters.")]
    public string Name { get; set; }
    [Required]
    [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Phone number must be a valid 10-digit number starting with 7, 8, or 9.")]
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
    public string Gender { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
    public string Nationality { get; set; }

    public Guid BankId { get; set; }
    public Bank Bank { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();

}
