using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class ShoppingCartModel : PageModel
    {
        //same as before
        private IBookstoreRepository repo { get; set; }

        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public ShoppingCartModel (IBookstoreRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }


        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        //adds the item user selects and redirects to the onget above
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            //finds book associated w/ this id
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //if there is already a session set up it uses that session, if not it will create a new basket
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            //adds item to the basket
            basket.AddItem(b, 1);

            //sets json file based on the basket so it is retained page to page
            //HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        //associated with shoppingcart.cshtml page with the form that removes it
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new {ReturnUrl = returnUrl});
        }
    }
}
