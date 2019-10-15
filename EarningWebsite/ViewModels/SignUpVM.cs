using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EarningWebsite.ViewModels
{
    public class SignUpVM
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [EmailAddress, Required]
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Phone, Required]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public System.DateTime dob { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string payment_method { get; set; }
    }

    public class SignInVM
    {
        [EmailAddress, Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}