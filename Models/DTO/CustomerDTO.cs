using System.ComponentModel.DataAnnotations;

namespace UPIWork.Models.DTO
{
    public class CustomerDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed more than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Phone number must be a valid 10-digit number starting with 7, 8, or 9.")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email
        {
            get; set;
        }
        [Required]
        [StringLength(10, ErrorMessage = "Gender must be either 'Male', 'Female', or 'Other'.")]
        public string Gender { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
        public string Nationality { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        public Guid BankId { get; set; }

    }
}