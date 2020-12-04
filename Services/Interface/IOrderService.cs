using Dwagen.DTO.Orders;
using Dwagen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.Interface
{
    public interface IOrderService
    {
        /// <summary>
        /// Add new order into database
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        Task<CreationState> AddOrder(AddOrderDto orderDto);

        /// <summary>
        /// Get order from database by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDto>> GetOrdersByUserId(Guid userId);

        /// <summary>
        /// Update order Quantity and its date
        /// </summary>
        /// <param name="updateOrderDto"></param>
        /// <returns></returns>
        Task<bool> UpdateOrder(UpdateOrderDto updateOrderDto);
    }
}
