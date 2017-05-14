using OnlineShopMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote("ValidateUsername", "Home", ErrorMessage = "This username is already used.")]
        [MinLength(5, ErrorMessage = "Username must be at least 5 symbols")]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 symbols")]
        [DataType(DataType.Password)]
        [CustomPassword]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        [CustomPassword]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Remote("ValidateEmail", "Home", ErrorMessage = "This email is already used.")]
        public string Email { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
    }
}