using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be contain {2} - {1} characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only letters and spaces are allowed")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be contain {2} - {1} characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only letters and spaces are allowed")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Login must be contain {2} - {1} characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and digits are allowed")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "Inccorect E-Mail format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Must contain 1 upper, 1 lower and 1 digit and at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        [Display(Name = "Confirm")]
        public string ConfirmPassword { get; set; }
    }
}