using DataAccess;
using OnlineShopMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
namespace OnlineShopMVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View(); //home view
        }
        
        public ActionResult ReturnPCs()
        {
            return View();
        }

        public ActionResult ReturnLaptops()
        {
            return View();
        }

        public ActionResult ReturnSmartphones()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        
        public ActionResult ReturnCart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            // add the Cities to the Viewbag
            CityRepository cityRepository = new CityRepository();
            List<City> allCities = cityRepository.GetAll();
            ViewBag.AllCities = new SelectList(allCities, "ID", "Name");
            return View();
        }

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
        public ActionResult ValidateEmail(string email)
        {
            bool isEmailUsed = false;
            if (string.IsNullOrEmpty(email) == false)
            {
                UserRepository userRepo = new UserRepository();
                List<User> isUserExist = new List<User>();
                isUserExist = (userRepo.GetAll().Where(user => user.Email == email)).ToList();
                if (isUserExist.Capacity!=0)
                {
                    isEmailUsed = true;
                }
            }
            return Json(!isEmailUsed, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateUsername(string username)
        {
            bool isUsernameUsed = false;
            if (string.IsNullOrEmpty(username) == false)
            {
                UserRepository userRepo = new UserRepository();
                List<User> isUserExist = new List<User>();
                isUserExist = (userRepo.GetAll().Where(user => user.Username == username)).ToList();
                if (isUserExist.Capacity !=0)
                {
                    isUsernameUsed = true;
                }
            }
            return Json(!isUsernameUsed, JsonRequestBehavior.AllowGet);
        }
        
    }
}