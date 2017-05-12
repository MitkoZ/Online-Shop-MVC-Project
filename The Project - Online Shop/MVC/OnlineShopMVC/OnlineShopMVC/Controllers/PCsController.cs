using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using OnlineShopMVC.ViewModels;

namespace OnlineShopMVC.Controllers
{
    public class PCsController : Controller
    {
        public ActionResult Index(int categoryID)
        {
            ProductRepository productRepo = new ProductRepository();
            PCsRepository pcsRepo = new PCsRepository();
            List<Product> productsList=productRepo.GetAll().Where(item=>item.CategoryID==categoryID).ToList();
            List<PC> pcsList = pcsRepo.GetAll();
            List<PCsViewModel> pcsViewModel = new List<PCsViewModel>();
            foreach (Product product in productsList)
            {
                foreach (PC pc in pcsList)
                {
                    PCsViewModel newPC = new PCsViewModel(product, pc);
                    pcsViewModel.Add(newPC);
                    break;
                }
            }
            return View(pcsViewModel);
        }

        
        public ActionResult Details(int id)
        {
            ProductRepository productRepo = new ProductRepository();
            PCsRepository pcsRepository = new PCsRepository();
            Product product = productRepo.GetByID(id);
            PC pc = pcsRepository.GetByID(id);
            PCsViewModel pcViewModel = new PCsViewModel(product, pc);
            return View(pcViewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            PCsRepository pcsRepo = new PCsRepository();
            bool isDeleted1 = pcsRepo.DeleteByID(item=>item.ProductID==id);
            ProductRepository productRepo = new ProductRepository();
            bool isDeleted2 = productRepo.DeleteByID(id);

            if (isDeleted1 == false || isDeleted2 == false)
            {
                TempData["ErrorMessage"] = "Could not find a PC with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The PC was deleted successfully";
            }

            return RedirectToAction("Index","Home");
        }
    }
}