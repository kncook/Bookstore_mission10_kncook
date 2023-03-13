using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFShoppingCartRepository : IShoppingCartRepository
    {
        private BookstoreContext context;
        public EFShoppingCartRepository (BookstoreContext temp)
        {
            context = temp;
        }
        //code first gets all entries that are in ShoppingCarts then it gets info from the books and attaches it to that
        public IQueryable<ShoppingCart> ShoppingCarts => context.ShoppingCarts.Include(x => x.Lines).ThenInclude(x => x.Book);

        //when they save a donation, we pass in shopping cart then we go into the context file and aatach a range
        //where we then get the book associated with that line
        public void SaveShoppingCart(ShoppingCart cart)
        {
            context.AttachRange(cart.Lines.Select(x => x.Book));

            //if ID = 0 then we will add the donation id, then we will save the changes
            if (cart.CartId == 0)
            {
                context.ShoppingCarts.Add(cart);
            }

            context.SaveChanges();
        }
    }
}
