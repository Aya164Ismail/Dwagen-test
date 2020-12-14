using Dwagen.Model;
using Dwagen.Model.Entities;
using Dwagen.Repository.Generic;
using Dwagen.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Repository.implementation
{
    public class OrderRepository : GenericRepository<Orders>, IOrderRepository
    {

        public OrderRepository(DwagenContext context)
            : base(context)
        {
        }

        public async Task<Orders> GetOrdersAsync(Guid orderId)
        {
            return await _context.Orders.FindAsync(orderId);
            
        }
    }
}
