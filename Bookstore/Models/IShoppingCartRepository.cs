using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface IShoppingCartRepository
    {
        IQueryable<ShoppingCart> ShoppingCarts { get; }

        void SaveShoppingCart(ShoppingCart cart);
            
    }
}
