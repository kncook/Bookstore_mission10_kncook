using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo { get; set; } 

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string bookType, int pageNum = 1) // => View(); just doing this is same thing as the return, lambda is a shortcut
        {
            int pageSize = 10;
            //creating aew view model
            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookType || bookType == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                //casting info to the view to be able to use it
                PageInfo = new PageInfo
                {
                    //if the book category is null we will use the count, otherwise we will use the catgeory
                    //thsi will make it so it only shows 1 page at the bottom when there is really only 1 page
                    TotalNumBooks = (bookType == null
                        ? repo.Books.Count()
                        : repo.Books.Where(x => x.Category == bookType).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
            /* var book = repo.Books //.ToList();
                 .OrderBy(b => b.Title)
                 .Skip((pageNum - 1) * pageSize)
                 .Take(pageSize);*/ //use .Skip to skip the first amount or skip the page num
        }
    }
}
