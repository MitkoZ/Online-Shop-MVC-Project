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
                ProductRepository productRepo = new ProductRepository();
                Product product = productRepo.GetAll().FirstOrDefault(item => item.ID == productID);
                CartSession.Current.Products.Add(product);
                ViewBag.Message = "Product added to cart successfully!";
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Index");
        }

        public ActionResult DeleteProduct(int productID)
        {
            CartSession.Current.DeleteProduct(productID);
            ViewBag.Message = "Product deleted from the cart successfully!";
            return View("Index");
        }
        public ActionResult Order()
        {
            ViewBag.Message = "Order made successfully!";
            SalesRepository salesRepo = new SalesRepository();
            List<Product> products = CartSession.Current.Products;
            foreach (var product in products)
            {
                Sale sale = new Sale();
                sale.ProductID = product.ID;
                sale.UserID = LoginUserSession.Current.UserID;
                sale.DateBought = DateTime.Now;
                salesRepo.Save(sale);
            }
            
            return View("Index");
        }
    }
}