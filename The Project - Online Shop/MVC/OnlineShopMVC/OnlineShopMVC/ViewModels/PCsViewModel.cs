using DataAccess;
using OnlineShopMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.ViewModels
{
    public class PCsViewModel:ISearchItem
    {
        public List<PCsViewModel> pcsViewModel { get; set; }

        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PCsInfo { get; set; }//for the front view
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
        public string VideoCard { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public string CategoryType { get; set; }

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
            this.CategoryType = product.Category.Name;
        }

        public PCsViewModel(List<Product> pcProduct, List<PC> computers)
        {
            pcsViewModel = new List<PCsViewModel>();
            foreach (PC pc in computers.Where(pc=>pc.ProductID==pc.Product.ID))
            {
                PCsViewModel pcViewModel = new PCsViewModel();
                pcViewModel.ProductId = pc.ProductID;
                pcViewModel.CategoryID = pc.Product.CategoryID;
                pcViewModel.OS = pc.Product.OS;
                pcViewModel.Processor = pc.Product.Processor;
                pcViewModel.Name = pc.Product.Name;
                pcViewModel.RAM = pc.Product.RAM;
                pcViewModel.Storage = pc.Product.Storage;
                pcViewModel.VideoCard = pc.VideoCard;
                pcViewModel.PCsInfo = pc.Product.Name + " with processor " + pc.Product.Processor;
                pcViewModel.Price = (double)pc.Product.Price;
                pcViewModel.ImagePath = Path.Combine(Constants.ImagesPCsDirectory, pc.Product.ImageName);
                pcsViewModel.Add(pcViewModel);
            }
        }
    }
}