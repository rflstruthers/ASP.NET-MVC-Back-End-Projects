using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementPortal.Models;
using Microsoft.AspNet.Identity;

namespace ManagementPortal.Controllers
{
    public class CartItemController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // CartItem home view, gets users ID and shows CartItems that match that users ID
        // If no user is logged in, redirects to login view
        public ActionResult Index()
        {
            try
            {
                var userId = User.Identity.GetUserId().ToString();
                var userCartItems = db.CartItems.Where(c => c.User == userId);

                return View(userCartItems.ToList());
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // Add selected Product to CartItems
        // Check if Product already exists in CartItems, if so then add 1 to the Quantity, 
        // if not then create a new CartItem and add it to the CartItems table
        public ActionResult AddToCart(int id)
        {
            var userId = User.Identity.GetUserId().ToString();
            var cartItem = db.CartItems.SingleOrDefault(c => c.ProductId == id && c.User == userId);
            if (cartItem == null)
            {
                var cartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        CartItemId = Guid.NewGuid().ToString(),
                        User = userId,
                        Quantity = 1,
                        DateCreated = DateTime.Now,
                        ProductId = id,
                        Product = db.Products.SingleOrDefault(p => p.ProductId == id)
                    }
                };
                cartItems.ForEach(c => db.CartItems.Add(c));
            }
            else
            {
                cartItem.Quantity++;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        
        // Returns Details view for selected CartItem
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // If the Quantity is above 1, Quantity is decreased by 1. If Quantity is 1 (or below),
        // CartItem is removed from CartItems table.
        public ActionResult Decrease(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cartItem.Quantity <= 1)
                {
                    db.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity--;
                }    
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
       
        // Remove selected CartItem from CartItems table
        public ActionResult Delete(string id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // proceed to checkout with Products currently in the cart
        public ActionResult Checkout()
        {
            var userId = User.Identity.GetUserId().ToString();
            var userCartItems = db.CartItems.Where(c => c.User == userId);

            return View(userCartItems.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
