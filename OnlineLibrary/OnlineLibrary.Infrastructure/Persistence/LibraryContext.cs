using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Common;
using OnlineLibrary.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLibrary.Infrastructure.Persistence
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
