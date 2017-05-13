using DataAccess;
using OnlineShopMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class PCsViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PCsInfo { get; set; }//for the front view
        [Required]
        public double Price { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string OS { get; set; }
        [Required]
        public int RAM { get; set; }
        [Required]
        public int Storage { get; set; }
        [Required]
        public string VideoCard { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public PCsViewModel()
        {

        }

        public PCsViewModel(Product product, PC pc)
        {
            this.ProductId = product.ID;
            this.CategoryID = product.CategoryID;
            this.OS = product.OS;
            this.Processor = product.Processor;
            this.Name = product.Name;
            this.RAM = product.RAM;
            this.Storage = product.Storage;
            this.VideoCard = pc.VideoCard;
            this.PCsInfo = product.Name + " with processor " + product.Processor;
            this.Price = (double)product.Price;
            this.ImagePath = Path.Combine(Constants.ImagesPCsDirectory, product.ImageName);
        }
    }
}