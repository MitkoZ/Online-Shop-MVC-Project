using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.Helpers
{
    public class CartSession
    {
        public List<Product> Products { get; private set; }
        public static CartSession Current
        {
            get
            {
                CartSession cartSession = (CartSession)HttpContext.Current.Session["CartProducts"];
                if (cartSession==null)
                {
                    cartSession = new CartSession();
                    HttpContext.Current.Session["CartProducts"] = cartSession;
                }
                return cartSession;
            }
        }
        public CartSession()
        {
            this.Products = new List<Product>();
        }
        public void AddProduct(Product product)
        {
            this.Products.Add(product);
        }
        public void DeleteProduct(int id)
        {
            Products.RemoveAt(Products.FindIndex(item=>item.ID==id));
        }

        public void OnLogoutDelete()//deleting the cart if the user has logged out
        {
            this.Products.Clear();
        }
    }
}