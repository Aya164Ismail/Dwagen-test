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
            CreateMap<AddUserDto, UsersProfile>()
                .ReverseMap();

            CreateMap<LoginUserDto, UsersProfile>().ReverseMap();

            CreateMap<UserDto, UsersProfile>().ReverseMap();

        }
    }
}

