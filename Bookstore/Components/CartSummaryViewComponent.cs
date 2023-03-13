using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Basket basket;
        public CartSummaryViewComponent(Basket cartService)
        {
            basket = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(basket);
        }
    }
}


