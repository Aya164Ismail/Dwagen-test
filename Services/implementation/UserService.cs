﻿using AutoMapper;
using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Model.Entities;
using Dwagen.Repository;
using Dwagen.Repository.UnitOfWork;
using Dwagen.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.implementation
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreationState> AddUserAsnc(AddUserDto addUserDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };

            var checkUsersAddedBefore = await _unitOfWork.UserRepository.FindElementAsync(x => x.Email == addUserDto.Email || x.NumberPhone == addUserDto.NumberPhone);
            if(checkUsersAddedBefore == null)
            {
                var newUser = _mapper.Map<AddUserDto, Users>(addUserDto);
                await _unitOfWork.UserRepository.CreateAsync(newUser);
                creationState.IsCreatedSuccessfully = await _unitOfWork.SaveAsync() > 0;
                creationState.CreatedObjectId = newUser.Id;
            }
            else
            {
                creationState.ErrorMessages.Add("This user added before");
            }

            return creationState;
        }

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
            if(user != null)
            {
                var userDto = _mapper.Map<Users, UserDto>(user);
                return userDto;
            }
            return null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(string include)
        {
            var user = await _unitOfWork.UserRepository.GetAllUsers(include);
            if (user != null)
            {
                var userDto = _mapper.Map<IEnumerable<Users>, IEnumerable<UserDto>>(user);
                return userDto;
            }
            return null;
        }

        public async Task<bool> LoginUser(LoginUserDto loginUserDto)
        {
            //get user that mathes email or phoneNamber and password
            var getuser = await _unitOfWork.UserRepository.FindElementAsync(x => (x.Email == loginUserDto.Email || x.NumberPhone == x.NumberPhone) && x.Password == x.Password);
            if(getuser != null)
            {
                return true;
            }
            return false;
        }
    }
}