using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Contracts.Persistence
{
    public interface IBookRepository : IAsyncRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCriteria(string author, string publisher, string name);
    }
}
