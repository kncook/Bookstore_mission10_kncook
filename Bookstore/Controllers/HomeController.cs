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

        public IActionResult Index(int pageNum = 1) // => View(); just doing this is same thing as the return, lambda is a shortcut
        {
            int pageSize = 10;
            //creating aew view model
            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                //casting info to the view to be able to use it
                PageInfo = new PageInfo
                {
                    TotalNumBooks = repo.Books.Count(),
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
