using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using OnlineShopMVC.Helpers;
using System.IO;

namespace OnlineShopMVC.ViewModels
{
    public class SmartphonesViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SmartphonesInfo { get; set; }//for the front view
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Processor { get; set; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string Camera { get; set; }
        public string SIMCardType { get; set; }
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
            this.ImagePath = Path.Combine(Constants.ImagesSmartphonesDirectory, product.ImageName);
        }
    }
}