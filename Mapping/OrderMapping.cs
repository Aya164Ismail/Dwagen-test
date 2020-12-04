using AutoMapper;
using Dwagen.DTO.Orders;
using Dwagen.Model.Entities;
using System.Threading.Tasks;

namespace Dwagen.Mapping
{
    public partial class AutoMapperProfile : Profile
    {
        public void OrderMapping()
        {
            CreateMap<AddOrderDto, Orders>()
                .ReverseMap();

            CreateMap<OrderDto, Orders>()
                .ReverseMap();

            CreateMap<UpdateOrderDto, Orders>()
                .ReverseMap();

        }
    }
}
