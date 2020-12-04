using AutoMapper;
using Dwagen.DTO.Products;
using Dwagen.Model.Entities;

namespace Dwagen.Mapping
{
    public partial class AutoMapperProfile : Profile
    {
        public void ProductMapping()
        {
            CreateMap<AddProductDto, Products>()
                .ReverseMap();

            CreateMap<UpdateProductDto, Products>()
                .ReverseMap();

            CreateMap<ProductDto, Products>()
                .ReverseMap();
            //c50b2437-c894-44ac-547d-08d8933cf391
        }
    }
}
