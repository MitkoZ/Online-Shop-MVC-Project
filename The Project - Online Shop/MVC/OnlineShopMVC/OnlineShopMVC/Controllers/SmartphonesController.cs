using DataAccess;
using OnlineShopMVC.Helpers;
using OnlineShopMVC.ViewModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.Controllers
{
    public class SmartphonesController : Controller
    {
        [AllowAnonymous]//allows access to all kind of users
        public ActionResult Index(int categoryID, string sortColumn, string direction)
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
            IQueryable<SmartphonesViewModel> records = smartphonesViewModel.AsQueryable();
            string sortColDirection = sortColumn + direction;
            switch (sortColDirection)
            {
                case "Price":
                    records = records.OrderBy(record => record.Price);
                    break;
                case "PriceDesc":
                    records = records.OrderByDescending(record => record.Price);
                    break;
                case "NameDesc":
                    records = records.OrderByDescending(record => record.Name);
                    break;
                default:
                    records = records.OrderBy(record => record.Name);
                    break;
            }
            List<SmartphonesViewModel> recordsToList = records.ToList();
            SearchViewModel<SmartphonesViewModel> searchViewModel = new SearchViewModel<SmartphonesViewModel>(recordsToList);
            searchViewModel.LastSortColumn = sortColumn;
            searchViewModel.LastSortDirection = direction;
            return View(searchViewModel);
        }


        [HttpGet]
        [CustomAuthorize]//allows access only to admins or if a given user has access
        public ActionResult Edit(int ProductId = 0)
        {
            SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel();
            ProductRepository productRepo = new ProductRepository();
            Product dbProduct = productRepo.GetAll(item => item.ID == ProductId);
            SmartphonesRepository smartphonesRepo = new SmartphonesRepository();
            Smartphone dbSmartphone = smartphonesRepo.GetAll(item => item.ProductID == ProductId);
            if (dbProduct != null && dbSmartphone != null)
            {
                smartphoneViewModel = new SmartphonesViewModel(dbProduct, dbSmartphone);
            }
            return View(smartphoneViewModel);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Edit(SmartphonesViewModel viewModel)
        {
            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "Ooooops, a serious error occured: No ViewModel.";
                return RedirectToAction("Index","Home");
            }
            ProductRepository productRepo = new ProductRepository();
            Product dbProduct = productRepo.GetAll(item => item.ID == viewModel.ProductId);
            SmartphonesRepository smartphonesRepo = new SmartphonesRepository();
            Smartphone dbSmartphone = smartphonesRepo.GetAll(item => item.ProductID == viewModel.ProductId);
            if (dbProduct == null)
            {
                dbProduct = new Product();
            }

            if (dbSmartphone == null)
            {
                dbSmartphone = new Smartphone();
            }
           
            dbProduct.Name = viewModel.Name;
            dbProduct.OS = viewModel.OS;
            dbProduct.Price = (decimal)viewModel.Price;
            dbProduct.Processor = viewModel.Processor;
            dbProduct.RAM = viewModel.RAM;
            dbProduct.Storage = viewModel.Storage;
            dbSmartphone.Camera = viewModel.Camera;
            dbSmartphone.SIMCardType = viewModel.SIMCardType;
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
            {
                string imagesPath = Server.MapPath(Constants.ImagesSmartphonesDirectory);
                string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                file.SaveAs(savedFileName);
                dbProduct.ImageName = uniqueFileName;
            }
            dbProduct.CategoryID = 3;
            productRepo.Save(dbProduct);
            dbSmartphone.ProductID = dbProduct.ID;
            smartphonesRepo.Save(dbSmartphone);
            TempData["Message"] = "The smartphone was saved successfully";
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            ProductRepository productRepo = new ProductRepository();
            SmartphonesRepository pcsRepository = new SmartphonesRepository();
            Product product = productRepo.GetByID(id);
            Smartphone smartphone = pcsRepository.GetAll(item=>item.ProductID==id);
            SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel(product, smartphone);
            return View(smartphoneViewModel);
        }
        [CustomAuthorize] //you need to be authenticated and have given access to pass
        public ActionResult Delete(int id = 0)
        {
            SmartphonesRepository smartphonesRepo = new SmartphonesRepository();
            bool isDeleted1 = smartphonesRepo.DeleteByID(item => item.ProductID == id);
            ProductRepository productRepo = new ProductRepository();
            bool isDeleted2 = productRepo.DeleteByID(id);

            if (isDeleted1 == false || isDeleted2 == false)
            {
                TempData["ErrorMessage"] = "Could not find a smartphone with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The smartphone was deleted successfully";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}