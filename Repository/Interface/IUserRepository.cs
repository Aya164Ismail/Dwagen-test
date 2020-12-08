using Dwagen.Model.Entities;
using Dwagen.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Repository.Interface
{
    public interface IUserRepository : IGenericRepository<UsersProfile>
    {
        /// <summary>
        /// get all users in the system
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<IEnumerable<UsersProfile>> GetAllUsers(string include);
    }
}
