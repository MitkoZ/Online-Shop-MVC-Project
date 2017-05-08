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
        // GET: PCs
        public ActionResult Index()
        {
            ProductRepository productRepo = new ProductRepository();
            PCsRepository pcsRepo = new PCsRepository();
            List<Product> productsList=productRepo.GetAll();
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
    }
}