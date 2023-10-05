using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Contracts.Persistence
{
   
        public interface IJwtTokenHandler
        {
            string GenerateToken(User user);
        }
    
}
