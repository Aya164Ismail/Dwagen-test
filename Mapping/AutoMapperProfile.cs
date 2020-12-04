using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwagen.Mapping
{
    public partial class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMapping();
            ProductMapping();
            OrderMapping();
        }
    }
}
