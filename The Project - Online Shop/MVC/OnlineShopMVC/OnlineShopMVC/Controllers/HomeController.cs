using DataAccess;
using OnlineShopMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using OnlineShopMVC.Helpers;

namespace OnlineShopMVC.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(); //home view
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            CartSession.Current.OnLogoutDelete();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // here we have to check if the username exists in the database
                UnitOfWork unitOfWork = new UnitOfWork();
                User dbUser = unitOfWork.UserRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.Username,dbUser.IsAdmin);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Search(string keywords)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Product> pcProduct = unitOfWork.ProductRepository.GetAll(pc => pc.CategoryID == 1 || pc.CategoryID == 2);
            List<Product> smartphoneProduct= unitOfWork.ProductRepository.GetAll(smartphone => smartphone.CategoryID == 3);
            List<PC> computers= unitOfWork.PCsRepository.GetAll();
            List<Smartphone> smartphones= unitOfWork.SmartphonesRepository.GetAll();
            PCsViewModel computersViewModel = new PCsViewModel(pcProduct, computers);
            SmartphonesViewModel smartphonesViewModel = new SmartphonesViewModel(smartphoneProduct, smartphones);
            IQueryable<PCsViewModel> pcRecords=computersViewModel.pcsViewModel.AsQueryable();
            IQueryable<SmartphonesViewModel> smartphoneRecords= smartphonesViewModel.smartphonesViewModel.AsQueryable();

            if (!string.IsNullOrEmpty(keywords))
            {
                pcRecords = pcRecords.Where(record => record.PCsInfo.ToLower().Contains(keywords.ToLower()));
                smartphoneRecords = smartphoneRecords.Where(record => record.SmartphonesInfo.ToLower().Contains(keywords.ToLower()));
            }
            List<PCsViewModel> pcRecordsToList = pcRecords.ToList();
            List<SmartphonesViewModel> smartphoneRecordsToList = smartphoneRecords.ToList();
            AllItemsSearchEngineViewModel allItemsViewModel = new AllItemsSearchEngineViewModel(pcRecordsToList, smartphoneRecordsToList);
            return View(allItemsViewModel);
        }

        public ActionResult ReturnCart()
        {
             return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<City> allCities = unitOfWork.CityRepository.GetAll();
            ViewBag.AllCities = new SelectList(allCities, "ID", "Name");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                User existingDbUser = unitOfWork.UserRepository.GetUserByName(viewModel.Username);
                if (existingDbUser != null)
                {
                    ModelState.AddModelError("", "This user is already registered in the system!");
                    return View();
                }
                User dbUser = new DataAccess.User();
                dbUser.Username = viewModel.Username;
                dbUser.Name = viewModel.Name;
                dbUser.Email = viewModel.Email;
                dbUser.CityID = viewModel.CityID;
                dbUser.Address = viewModel.Address;
                dbUser.CardNumber = viewModel.CardNumber;
                dbUser.IsAdmin = false;
                unitOfWork.UserRepository.RegisterUser(dbUser, viewModel.Password);
                bool isSaved=unitOfWork.Save()>0;
                if (isSaved)
                {
                    TempData["Message"] = "User was registered successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Ooops something went wrong";
                }
                return RedirectToAction("Index");
            }
            else
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                List<City> allCities = unitOfWork.CityRepository.GetAll();
                ViewBag.AllCities = new SelectList(allCities, "ID", "Name");
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult ValidateEmail(string email)
        {
            bool isEmailUsed = false;
            if (string.IsNullOrEmpty(email) == false)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                User isUserExist = new User();
                isUserExist = unitOfWork.UserRepository.GetAll().FirstOrDefault(user => user.Email == email);
                if (!(isUserExist==null))
                {
                    isEmailUsed = true;
                }
            }
            return Json(!isEmailUsed, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult ValidateUsername(string username)
        {
            bool isUsernameUsed = false;
            if (string.IsNullOrEmpty(username) == false)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                User isUserExist = new User();
                isUserExist = (unitOfWork.UserRepository.GetAll().FirstOrDefault(user => user.Username == username));
                if (!(isUserExist == null))
                {
                    isUsernameUsed = true;
                }
            }
            return Json(!isUsernameUsed, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(CityViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                ModelState.AddModelError("", "Please enter a valid city name");
                return View();
            }
            if (ModelState.IsValid)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                City cityInput = new City();
                cityInput.Name = viewModel.Name;
                unitOfWork.CityRepository.Save(cityInput);
                bool isAdded = unitOfWork.Save() > 0;
                if (isAdded)
                {
                    TempData["Message"] = "City added successfully";
                }
                else
                {
                    TempData["Message"] = "Ooops something went wrong";
                }
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("AddCity","Home");
        }
        
        public ActionResult ValidateCity(string Name)
        {
            bool isCityExist = false;
            if (string.IsNullOrEmpty(Name) == false)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                City dbCity = new City();
                dbCity = unitOfWork.CityRepository.GetAll().FirstOrDefault(category => category.Name == Name);
                if (!(dbCity == null))
                {
                    isCityExist = true;
                }
            }
            return Json(!isCityExist, JsonRequestBehavior.AllowGet);
        }
    }
}