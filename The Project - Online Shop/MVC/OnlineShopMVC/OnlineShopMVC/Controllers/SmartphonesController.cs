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
        public ActionResult Index(int categoryID, string sortColumn, string direction,string keywords, int pageSize = Constants.DefaultPageSize, int pageIndex = 1)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Product> productsList = unitOfWork.ProductRepository.GetAll().Where(item=>item.CategoryID==categoryID).ToList();
            List<Smartphone> smartphonesList = unitOfWork.SmartphonesRepository.GetAll();
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
            if (!string.IsNullOrEmpty(keywords))
            {
                records = records.Where(record => record.SmartphonesInfo.ToLower().Contains(keywords.ToLower()));
            }
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
            List<SmartphonesViewModel> recordsToList = records
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int allRecordsCount = records.Count();
            SearchViewModel<SmartphonesViewModel> searchViewModel = new SearchViewModel<SmartphonesViewModel>(recordsToList,pageSize,pageIndex, allRecordsCount, sortColumn, direction);
            return View(searchViewModel);
        }


        [HttpGet]
        [CustomAuthorize]//allows access only to admins or if a given user has access
        public ActionResult Edit(int ProductId = 0)
        {
            SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel();
            UnitOfWork unitOfWork = new UnitOfWork();
            Product dbProduct = unitOfWork.ProductRepository.GetFirst(item => item.ID == ProductId);
            Smartphone dbSmartphone = unitOfWork.SmartphonesRepository.GetFirst(item => item.ProductID == ProductId);
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
                return RedirectToAction("Index", "Home");
            }
            HttpPostedFileBase file = Request.Files[0];
            if (string.IsNullOrEmpty(file.FileName) && string.IsNullOrEmpty(viewModel.ImagePath))
            {
                ModelState.AddModelError("", "Please add an image");
            }
            if (ModelState.IsValid)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                Product dbProduct = unitOfWork.ProductRepository.GetFirst(item => item.ID == viewModel.ProductId);
                Smartphone dbSmartphone = unitOfWork.SmartphonesRepository.GetFirst(item => item.ProductID == viewModel.ProductId);
                if (dbProduct == null)
                {
                    dbProduct = new Product();
                }

                if (dbSmartphone == null)
                {
                    dbSmartphone = new Smartphone();
                }
                dbProduct.CategoryID = viewModel.CategoryID;
                dbProduct.Name = viewModel.Name;
                dbProduct.OS = viewModel.OS;
                dbProduct.Price = (decimal)viewModel.Price;
                dbProduct.Processor = viewModel.Processor;
                dbProduct.RAM = viewModel.RAM;
                dbProduct.Storage = viewModel.Storage;
                dbSmartphone.Camera = viewModel.Camera;
                dbSmartphone.SIMCardType = viewModel.SIMCardType;
                
                if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
                {
                    string imagesPath = Server.MapPath(Constants.ImagesSmartphonesDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                    string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                    file.SaveAs(savedFileName);
                    dbProduct.ImageName = uniqueFileName;
                }
                dbProduct.Smartphones.Add(dbSmartphone);
                unitOfWork.ProductRepository.Save(dbProduct);
                bool isSaved=unitOfWork.Save()>0;
                
                if (isSaved)
                {
                    TempData["Message"] = "The smartphone was saved successfully";
                }

                return RedirectToAction("Index", "Home");
            }
            
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Product product = unitOfWork.ProductRepository.GetByID(id);
            Smartphone smartphone = unitOfWork.SmartphonesRepository.GetFirst(item=>item.ProductID==id);
            SmartphonesViewModel smartphoneViewModel = new SmartphonesViewModel(product, smartphone);
            return View(smartphoneViewModel);
        }

        [CustomAuthorize] //you need to be authenticated and have given access to pass
        public ActionResult Delete(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            string imageName = unitOfWork.ProductRepository.GetFirst(item => item.ID == id).ImageName;
            string imagePath=Path.Combine(Server.MapPath(Constants.ImagesSmartphonesDirectory), imageName);
            System.IO.File.Delete(imagePath);
            unitOfWork.SmartphonesRepository.DeleteByPredicate(item => item.ProductID == id);
            unitOfWork.ProductRepository.DeleteByID(id);
            bool isDeleted = unitOfWork.Save() > 0;
            if (isDeleted==false)
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