using Dwagen.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwagen.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Service Repositories
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }

        /// <summary>
        /// Save Changes To Database
        /// </summary>
        int Save();
        /// <summary>
        /// Save Changes To Database Asynchronous
        /// </summary>
        Task<int> SaveAsync();
    }
}
