using AutoMapper;
using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwagen.Mapping
{
    public partial class AutoMapperProfile : Profile
    {
        public void UserMapping()
        {
            CreateMap<AddUserDto, Users>()
                .ReverseMap();

            CreateMap<LoginUserDto, Users>().ReverseMap();

            CreateMap<UserDto, Users>().ReverseMap();

        }
    }
}

