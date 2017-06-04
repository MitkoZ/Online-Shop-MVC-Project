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
    public class SmartphonesViewModel:ISearchItem
    {
        public List<SmartphonesViewModel> smartphonesViewModel { get; set; }

        public int ProductId { get; set; }
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string SmartphonesInfo { get; set; }//for the front view
        [Required]
        public double Price { get; set; }
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
        public string CategoryType { get; set; }
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }

        public SmartphonesViewModel(Product product, Smartphone smartphone)
        {
            this.ProductId = product.ID;
            this.CategoryID = product.CategoryID;
            this.OS = product.OS;
            this.Processor = product.Processor;
            this.Name = product.Name;
            this.RAM = product.RAM;
            this.Storage = product.Storage;
            this.SmartphonesInfo = product.Name + " with processor " + product.Processor;
            this.Price = (double)product.Price;
            this.Camera = smartphone.Camera;
            this.SIMCardType = smartphone.SIMCardType;
            this.ImagePath = Path.Combine(Constants.ImagesSmartphonesDirectory, product.ImageName);
            this.CategoryType = product.Category.Name;
        }
        public SmartphonesViewModel()
        {

        }

        public SmartphonesViewModel(List<Product> smartphoneProduct, List<Smartphone> smartphones)
        {
            smartphonesViewModel = new List<SmartphonesViewModel>();
            foreach (Smartphone smartphone in smartphones.Where(smartphone=>smartphone.ProductID==smartphone.Product.ID))
            {
                SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel();
                smartphoneViewModel.ProductId = smartphone.ProductID;
                smartphoneViewModel.OS = smartphone.Product.OS;
                smartphoneViewModel.Processor = smartphone.Product.Processor;
                smartphoneViewModel.Name = smartphone.Product.Name;
                smartphoneViewModel.RAM = smartphone.Product.RAM;
                smartphoneViewModel.Storage = smartphone.Product.Storage;
                smartphoneViewModel.SmartphonesInfo = "Smartphone " + smartphone.Product.Name + " with processor " + smartphone.Product.Processor;
                smartphoneViewModel.Price = (double)smartphone.Product.Price;
                smartphoneViewModel.Camera = smartphone.Camera;
                smartphoneViewModel.SIMCardType = smartphone.SIMCardType;
                smartphoneViewModel.ImagePath = Path.Combine(Constants.ImagesSmartphonesDirectory, smartphone.Product.ImageName);
                smartphonesViewModel.Add(smartphoneViewModel);
            }
        }
    }
}