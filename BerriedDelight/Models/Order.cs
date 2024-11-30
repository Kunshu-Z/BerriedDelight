using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BerriedDelight.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your Address")]
        [StringLength(100)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter your Zip Code")]
        [StringLength(4)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your City")]
        [StringLength(25)]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your State")]
        [StringLength(25)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your Country")]
        [StringLength(50)]
        [Display(Name = "Country")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your 16-digit card number")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be exactly 16 digits")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be exactly 16 digits and numeric")]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter card expiry date")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Expiry date must be in MM/YY format")]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Expiry")]
        public string Expiry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your 3-digit CVV")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Card number must be exactly 3 digits")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Card number must be exactly 3 digits and numeric")]
        [Display(Name = "CVV")]
        public string CVV { get; set; } = string.Empty;

        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!IsValidExpiryDate(Expiry))
            {
                validationResults.Add(new ValidationResult("Expiry date must be in MM/YY format and cannot be in the past", new[] { nameof(Expiry) }));
            }

            return validationResults;
        }

        private bool IsValidExpiryDate(string expiryDate)
        {
            if (!Regex.IsMatch(expiryDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                return false;
            }

            var month = int.Parse(expiryDate.Substring(0, 2));
            var year = int.Parse(expiryDate.Substring(3, 2)) + 2000;

            var lastDayOfExpiryMonth = DateTime.DaysInMonth(year, month);
            var dateOfExpiry = new DateTime(year, month, lastDayOfExpiryMonth, 23, 59, 59);

            return dateOfExpiry > DateTime.Now;
        }
    }
}
