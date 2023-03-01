using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    //interface is a templet for a class that ensures it will be used correctly
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; }
    }
}
