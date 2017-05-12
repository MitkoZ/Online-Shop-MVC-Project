using DataAccess;
using OnlineShopMVC.ViewModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.Controllers
{
    public class SmartphonesController : Controller
    {
        public ActionResult Index(int categoryID)
        {
            ProductRepository productRepo = new ProductRepository();
            SmartphonesRepository smartphonesRepo = new SmartphonesRepository();
            List<Product> productsList = productRepo.GetAll().Where(item=>item.CategoryID==categoryID).ToList();
            List<Smartphone> smartphonesList = smartphonesRepo.GetAll();
            List<SmartphonesViewModel> smartphonesViewModel = new List<SmartphonesViewModel>();
            foreach (Product product in productsList)
            {
                foreach (Smartphone smartphone in smartphonesList)
                {
                    SmartphonesViewModel newSmartphone = new SmartphonesViewModel(product, smartphone);
                    smartphonesViewModel.Add(newSmartphone);
                    break;
                }
            }
            return View(smartphonesViewModel);
        }

        public ActionResult Details(int id)
        {
            ProductRepository productRepo = new ProductRepository();
            SmartphonesRepository pcsRepository = new SmartphonesRepository();
            Product product = productRepo.GetByID(id);
            Smartphone smartphone = pcsRepository.GetByID(id);
            SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel(product, smartphone);
            return View(smartphoneViewModel);
        }
    }
}