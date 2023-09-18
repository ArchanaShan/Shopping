using System.ComponentModel.DataAnnotations;

namespace Shopping.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "This field is required.")]

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid integer.")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "This field is required.")]
        public string Address { get; set; } = null!;
    }
}
