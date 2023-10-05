using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LibraryContext dbContext) : base(dbContext)
        {
        }
       
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                                 .Where(u => u.Username == username)
                                 .FirstOrDefaultAsync();
        }
    }
}
