using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PhaseOne.Models;
using PhaseOne.viewModel;
using System.IO;

namespace PhaseOne.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        
        public void Addtocart(int id)
        {
            var context = new ApplicationDbContext();
            DateTime now = DateTime.Now;
            Cart cart = new Cart
            {
                ProductId = id,
                added_at = now
            };
            
            context.Cart.Add(cart);
            context.SaveChanges();

        }
        public void Removefromcart(int id)
        {
            var context = new ApplicationDbContext();
            Cart cart = new Cart();
            cart = context.Cart.Where(x => x.ProductId == id).Single();

            context.Cart.Remove(cart);
            context.SaveChanges();

        }
    }
}