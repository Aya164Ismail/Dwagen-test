using Dwagen.DTO.Orders;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Controllers
{
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        ILogger<OrdersController> _logger;
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// add new order in the system
        /// </summary>
        /// <param name="addOrderDto"></param>
        /// <returns></returns>
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDto addOrderDto)
        {
            try
            {
                var result = await _orderService.AddOrder(addOrderDto);
                if (result.IsCreatedSuccessfully)
                {
                    _logger.LogInformation("Adding New order");
                    return Ok(result);
                }
                _logger.LogError(result.ErrorMessages.FirstOrDefault());
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get order by user Id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetOrderByUserId")]
        public async Task<IActionResult> GetProductByUserId(Guid userId)
        {
            try
            {
                var result = await _orderService.GetOrdersByUserId(userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// update orders
        /// </summary>
        /// <param name="updateOrderDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                if (updateOrderDto != null)
                {
                    var result = await _orderService.UpdateOrder(updateOrderDto);
                    if (result)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
