using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//THis controller is specifically for the shopping cart 
namespace Bookstore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepository repo { get; set; }
        private Basket basket { get; set; }
        public ShoppingCartController (IShoppingCartRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        //when they click checkout to get them to the point of the form
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new ShoppingCart());
        }

        //when they click the post button after filling out checkout form
        [HttpPost]
        public IActionResult Checkout(ShoppingCart cart)
        {
            //if there is nothing in their basket, the cart is empty so we do not want them to checkout!
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty!");
            }

            if (ModelState.IsValid)
            {
                //gets the items in the basket and adds them to the arrays. Then saves it and clears the basket and returns to view
                cart.Lines = basket.Items.ToArray();
                repo.SaveShoppingCart(cart);
                basket.ClearBasket();

                return RedirectToPage("/ShoppingCartCompleted");
            }

            else
            {
                return View();
            }
        }
    }
}
