using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using OnlineShopMVC.Helpers;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.ViewModels
{
    public class SmartphonesViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string SmartphonesInfo { get; set; }//for the front view
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
        public string Camera { get; set; }
        [Required]
        public string SIMCardType { get; set; }
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }
        public SmartphonesViewModel(Product product, Smartphone smartphone)
        {
            this.ProductId = product.ID;
            this.OS = product.OS;
            this.Processor = product.Processor;
            this.Name = product.Name;
            this.RAM = product.RAM;
            this.Storage = product.Storage;
            this.SmartphonesInfo = "Smartphone " + product.Name + " with processor " + product.Processor;
            this.Price = (double)product.Price;
            this.Camera = smartphone.Camera;
            this.SIMCardType = smartphone.SIMCardType;
            this.ImagePath = Path.Combine(Constants.ImagesSmartphonesDirectory, product.ImageName);
        }
        public SmartphonesViewModel()
        {

        }
    }
}