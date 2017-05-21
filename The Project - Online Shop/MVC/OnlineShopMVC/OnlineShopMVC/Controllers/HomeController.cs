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
                UserRepository userRepository = new UserRepository();
                User dbUser = userRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

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
            PCsRepository pcRepo = new PCsRepository();
            SmartphonesRepository smartphoneRepo = new SmartphonesRepository();
            ProductRepository productRepo = new ProductRepository();
            List<Product> pcProduct = productRepo.GetAll(pc => pc.CategoryID == 1 || pc.CategoryID == 2);
            List<Product> smartphoneProduct=productRepo.GetAll(smartphone => smartphone.CategoryID == 3);
            List<PC> computers=pcRepo.GetAll();
            List<Smartphone> smartphones=smartphoneRepo.GetAll();
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
            // add the Cities to the Viewbag
            CityRepository cityRepository = new CityRepository();
            List<City> allCities = cityRepository.GetAll();
            ViewBag.AllCities = new SelectList(allCities, "ID", "Name");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                User existingDbUser = userRepository.GetUserByName(viewModel.Username);
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
                userRepository.RegisterUser(dbUser, viewModel.Password);
                TempData["Message"] = "User was registered successfully";
                return RedirectToAction("Index");
            }
            else
            {
                CityRepository cityRepository = new CityRepository();
                List<City> allCities = cityRepository.GetAll();
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
                UserRepository userRepo = new UserRepository();
                User isUserExist = new User();
                isUserExist = userRepo.GetAll().FirstOrDefault(user => user.Email == email);
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
                UserRepository userRepo = new UserRepository();
                User isUserExist = new User();
                isUserExist = (userRepo.GetAll().FirstOrDefault(user => user.Username == username));
                if (!(isUserExist == null))
                {
                    isUsernameUsed = true;
                }
            }
            return Json(!isUsernameUsed, JsonRequestBehavior.AllowGet);
        }
        
    }
}