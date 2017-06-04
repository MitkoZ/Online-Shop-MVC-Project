using DataAccess;
using OnlineShopMVC.Helpers;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopMVC.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult AddToCart(int productID)
        {
            if (LoginUserSession.Current.IsAuthenticated)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                Product product = unitOfWork.ProductRepository.GetAll().FirstOrDefault(item => item.ID == productID);
                CartSession.Current.Products.Add(product);
                ViewBag.Message = "Product added to cart successfully!";
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Index");
        }

        public ActionResult DeleteProduct(int productID)//delete a product from the cart
        {
            CartSession.Current.DeleteProduct(productID);
            ViewBag.Message = "Product deleted from the cart successfully!";
            return View("Index");
        }
        public ActionResult Order()
        {
            ViewBag.Message = "Order made successfully!";
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Product> products = CartSession.Current.Products;
            foreach (var product in products)
            {
                Sale sale = new Sale();
                sale.ProductID = product.ID;
                sale.UserID = LoginUserSession.Current.UserID;
                sale.DateBought = DateTime.Now;
                unitOfWork.SalesRepository.Save(sale);
            }
            CartSession.Current.OnLogoutDelete();
            return View("Index");
        }
    }
}