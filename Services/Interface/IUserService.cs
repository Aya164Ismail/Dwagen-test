using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Add new user 
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        Task<CreationState> AddUserAsnc(AddUserDto addUserDto);

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUserById(Guid userId);

        /// <summary>
        /// get all users in the system
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetAllUsers(string include);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        Task<bool> LoginUser(LoginUserDto loginUserDto);

        /// <summary>
        /// Identity
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        Task<CreationState> AddUserAsync(AddUserDto UserDto);
    }
}
