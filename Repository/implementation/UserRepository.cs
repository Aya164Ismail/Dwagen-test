using Dwagen.Model;
using Dwagen.Model.Entities;
using Dwagen.Repository.Generic;
using Dwagen.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Repository.implementation
{
    public class UserRepository : GenericRepository<UsersProfile>, IUserRepository
    {
        public UserRepository(DwagenContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<UsersProfile>> GetAllUsers(string include)
        {
            var users = await _context.Users.MultiInclude(include).ToListAsync();
            return (IEnumerable<UsersProfile>)users;
        }
    }
}
