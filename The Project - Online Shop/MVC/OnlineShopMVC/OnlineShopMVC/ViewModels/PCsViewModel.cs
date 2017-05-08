using DataAccess;
using OnlineShopMVC.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class PCsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PCsInfo { get; set; }//for the front view
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Processor { get; set; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string VideoCard { get; set; }
        public PCsViewModel(Product product, PC pc)
        {
            this.ProductId = product.ID;
            this.OS = product.OS;
            this.Processor = product.Processor;
            this.Name = product.Name;
            this.RAM = product.RAM;
            this.Storage = product.Storage;
            this.VideoCard = pc.VideoCard;
            this.PCsInfo = "PC "+product.Name + "with processor " + product.Processor;
            this.Price = (double)product.Price;
            this.ImagePath = Path.Combine(Constants.ImagesPCsDirectory, product.ImageName);
        }
    }
}