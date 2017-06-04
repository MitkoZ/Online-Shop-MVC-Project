using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using OnlineShopMVC.ViewModels;
using System.IO;
using OnlineShopMVC.Helpers;

namespace OnlineShopMVC.Controllers
{
    public class PCsController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(int categoryID, string sortColumn, string direction, string keywords, int pageSize = Constants.DefaultPageSize, int pageIndex = 1)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Product> productsList = unitOfWork.ProductRepository.GetAll().Where(item => item.CategoryID == categoryID).ToList();
            List<PC> pcsList = unitOfWork.PCsRepository.GetAll();
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
            IQueryable<PCsViewModel> records = pcsViewModel.AsQueryable();
            if (!string.IsNullOrEmpty(keywords))
            {
                records = records.Where(record => record.PCsInfo.ToLower().Contains(keywords.ToLower()));
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
            List<PCsViewModel> recordsToList = records
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int allRecordsCount = records.Count();
            SearchViewModel <PCsViewModel> searchViewModel = new SearchViewModel<PCsViewModel>(recordsToList,pageSize,pageIndex, allRecordsCount, sortColumn, direction);
            return View(searchViewModel);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Edit(int ProductId = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Category> allCategories = unitOfWork.CategoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories.Where(item =>item.Name!="Smartphones"), "ID", "Name");
            PCsViewModel pcViewModel = new PCsViewModel();
            Product dbProduct = unitOfWork.ProductRepository.GetFirst(item=>item.ID==ProductId);
            PC dbPC = unitOfWork.PCsRepository.GetFirst(item=>item.ProductID==ProductId);
            
            if (dbProduct != null && dbPC != null)
            {
                pcViewModel = new PCsViewModel(dbProduct,dbPC);
            }
            return View(pcViewModel);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Edit(PCsViewModel viewModel)
        {
            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "Ooooops, a serious error occured: No ViewModel.";
                return RedirectToAction("Index","Home");
            }
            HttpPostedFileBase file = Request.Files[0];
            if (string.IsNullOrEmpty(file.FileName) && string.IsNullOrEmpty(viewModel.ImagePath))
            {
                ModelState.AddModelError("", "Please add an image");
            }
            UnitOfWork unitOfWork = new UnitOfWork();
            if (ModelState.IsValid)
            {
                Product dbProduct = unitOfWork.ProductRepository.GetFirst(item => item.ID == viewModel.ProductId);
                PC dbPC = unitOfWork.PCsRepository.GetFirst(item => item.ProductID == viewModel.ProductId);
                if (dbProduct == null)
                {
                    dbProduct = new Product();
                }

                if (dbPC == null)
                {
                    dbPC = new PC();
                }
                dbProduct.CategoryID = viewModel.CategoryID;
                dbProduct.Name = viewModel.Name;
                dbProduct.OS = viewModel.OS;
                dbProduct.Price = (decimal)viewModel.Price;
                dbProduct.Processor = viewModel.Processor;
                dbProduct.RAM = viewModel.RAM;
                dbProduct.Storage = viewModel.Storage;
                dbPC.VideoCard = viewModel.VideoCard;
               
                if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
                {
                    string imagesPath = Server.MapPath(Constants.ImagesPCsDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                    string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                    file.SaveAs(savedFileName);
                    dbProduct.ImageName = uniqueFileName;
                }
                dbProduct.PCs.Add(dbPC);
                unitOfWork.ProductRepository.Save(dbProduct);
                bool isSaved=unitOfWork.Save()>0;
                if (isSaved && dbProduct.CategoryID == 1)
                {
                    TempData["Message"] = "The PC was saved successfully";
                }
                else if (isSaved)
                {
                    TempData["Message"] = "The laptop was saved successfully";
                }
                
                return RedirectToAction("Index", "Home");
            }
            List<Category> allCategories = unitOfWork.CategoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories.Where(item => item.Name != "Smartphones"), "ID", "Name");
            return View(viewModel);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Product product = unitOfWork.ProductRepository.GetByID(id);
            PC pc = unitOfWork.PCsRepository.GetFirst(item=>item.ProductID==id);
            PCsViewModel pcViewModel = new PCsViewModel(product, pc);
            return View(pcViewModel);
        }

        [CustomAuthorize]
        public ActionResult Delete(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            string imageName=unitOfWork.ProductRepository.GetFirst(item=>item.ID==id).ImageName;
            string imagePath = Path.Combine(Server.MapPath(Constants.ImagesPCsDirectory), imageName);
            System.IO.File.Delete(imagePath);
            unitOfWork.PCsRepository.DeleteByPredicate(item=>item.ProductID==id);
            unitOfWork.ProductRepository.DeleteByID(id);
            bool isDeleted = unitOfWork.Save() > 0;
            if (isDeleted==false)
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