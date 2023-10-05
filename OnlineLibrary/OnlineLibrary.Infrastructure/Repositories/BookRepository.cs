using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByCriteria(string author, string publisher, string name)
        {

            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author == author);
            }

            if (!string.IsNullOrEmpty(publisher))
            {
                query = query.Where(b => b.Publisher == publisher);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.Name == name);
            }

            return await query.ToListAsync();
          
        }
    }
}
