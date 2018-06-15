using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SmartMonitoring.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "User Name")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }

    public class UserRegister
    {
        
        //public string UserID { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string  FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        public string  LastName { get; set; }

        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [System.Web.Mvc.Remote("UserNameExist", "Home", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "User Name")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Spaces is not allowed in UserName !")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="User role")]
        public string UserRole { get; set; }

        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNo { get; set; }

        [Display(Name ="Fax Number")]
        public string  FaxNo { get; set; }

        [Required]
        [Display(Name ="Company name")]
        public string CompanyName { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string EmailID { get; set; }

        public bool isTrue
        { get { return true; } }

        [Required]
        //[Display(Name = "I agree to the terms and conditions")]
        [Compare("isTrue", ErrorMessage = "Please agree to Terms and Conditions")]
        public bool TermsAndConditions { get; set; }

        public Boolean UserStatus { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "The User ID is required")]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [System.Web.Mvc.Remote("CurrentPasswordVerify", "Home", HttpMethod = "POST", ErrorMessage = "Please enter correct password ! ")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}