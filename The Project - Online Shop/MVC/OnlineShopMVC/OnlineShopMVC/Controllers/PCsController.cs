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
            ProductRepository productRepo = new ProductRepository();
            PCsRepository pcsRepo = new PCsRepository();
            List<Product> productsList = productRepo.GetAll().Where(item => item.CategoryID == categoryID).ToList();
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
            SearchViewModel <PCsViewModel> searchViewModel = new SearchViewModel<PCsViewModel>(recordsToList,pageSize,pageIndex, allRecordsCount);
            searchViewModel.LastSortColumn = sortColumn;
            searchViewModel.LastSortDirection = direction;
            return View(searchViewModel);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Edit(int ProductId = 0)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories.Where(item =>item.Name!="Smartphones"), "ID", "Name");
            PCsViewModel pcViewModel = new PCsViewModel();
            ProductRepository productRepo = new ProductRepository();
            Product dbProduct = productRepo.GetFirst(item=>item.ID==ProductId);
            PCsRepository pcsRepo = new PCsRepository();
            PC dbPC = pcsRepo.GetFirst(item=>item.ProductID==ProductId);
            
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
            if (viewModel.ImagePath == null)
            {
                ModelState.AddModelError("", "Please add a picture for the product");
            }
            if (ModelState.IsValid)
            {
                ProductRepository productRepo = new ProductRepository();
                Product dbProduct = productRepo.GetFirst(item => item.ID == viewModel.ProductId);
                PCsRepository pcsRepo = new PCsRepository();
                PC dbPC = pcsRepo.GetFirst(item => item.ProductID == viewModel.ProductId);
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
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
                {
                    string imagesPath = Server.MapPath(Constants.ImagesPCsDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                    string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                    file.SaveAs(savedFileName);
                    dbProduct.ImageName = uniqueFileName;
                }

                productRepo.Save(dbProduct);
                dbPC.ProductID = dbProduct.ID;
                pcsRepo.Save(dbPC);
                if (dbProduct.CategoryID == 1)
                {
                    TempData["Message"] = "The PC was saved successfully";
                }
                else
                {
                    TempData["Message"] = "The laptop was saved successfully";
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Edit","PCs");
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            ProductRepository productRepo = new ProductRepository();
            PCsRepository pcsRepository = new PCsRepository();
            Product product = productRepo.GetByID(id);
            PC pc = pcsRepository.GetFirst(item=>item.ProductID==id);
            PCsViewModel pcViewModel = new PCsViewModel(product, pc);
            return View(pcViewModel);
        }

        [CustomAuthorize]
        public ActionResult Delete(int id = 0)
        {
            PCsRepository pcsRepo = new PCsRepository();
            bool isDeleted1 = pcsRepo.DeleteByPredicate(item=>item.ProductID==id);
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