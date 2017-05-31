using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.ViewModels
{
    public class CityViewModel
    {
        [Required]
        [Remote("ValidateCity", "Home", ErrorMessage = "This city is already added.")]
        public string Name { get; set; }
    }
}