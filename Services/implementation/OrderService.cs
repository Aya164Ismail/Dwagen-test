using AutoMapper;
using Dwagen.DTO.Orders;
using Dwagen.Model.Entities;
using Dwagen.Model.Enum;
using Dwagen.Repository;
using Dwagen.Repository.UnitOfWork;
using Dwagen.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.implementation
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreationState> AddOrder(AddOrderDto orderDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };

            var product = await _unitOfWork.ProductRepository.FindByIdAsync(orderDto.ProductId);
            if(orderDto.OrderQuantity <= product.ProductQuantity)
            {
                var newOrder = _mapper.Map<AddOrderDto, Orders>(orderDto);
                await _unitOfWork.OrderRepository.CreateAsync(newOrder);
                creationState.IsCreatedSuccessfully = await _unitOfWork.SaveAsync() > 0;
                creationState.CreatedObjectId = newOrder.Id;
            }
            else
            {
                creationState.ErrorMessages.Add("Invaled quantity of order");
            }
            return creationState;
        }
        
        public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(Guid userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetElementsAsync(x => x.UserId == userId);

            var orderDto = _mapper.Map<IEnumerable<Orders>, IEnumerable<OrderDto>>(orders);
            return orderDto;
        }

        public async Task<bool> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var order = await _unitOfWork.OrderRepository.FindByIdAsync(updateOrderDto.Id);
            if (order != null)
            {
                var product = await _unitOfWork.ProductRepository.FindByIdAsync(order.ProductId);
                if (updateOrderDto.OrderQuantity <= product.ProductQuantity && order.DeliveryState == DeliveryState.OrderBooker)
                {
                    var newOrder = _mapper.Map<UpdateOrderDto, Orders>(updateOrderDto);
                    _unitOfWork.OrderRepository.Update(newOrder);

                    return await _unitOfWork.SaveAsync() > 0;
                }
            }
                return false;
            
        }
    }
}
