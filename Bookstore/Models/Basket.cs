using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    /*model asscoiated w/ the shopping cart pages*/
    public class Basket
    {
        /*first part decalres, second part instansiates*/
        //Ability now to add an item to a list of items
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public void AddItem(Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();
            /* if what is in line, not in the cart yet we add a new entry in the list, otherwise we iwll just update the qty*/
            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
    //this allows the ability to get the total
    public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 25);
            return sum;
        }
    }



        public class BasketLineItem
        {
            public int LineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }

        }
    }

