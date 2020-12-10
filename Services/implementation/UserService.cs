using AutoMapper;
using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Model.Entities;
using Dwagen.Repository;
using Dwagen.Repository.UnitOfWork;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.implementation
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment
                            , UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<CreationState> AddUserAsnc(AddUserDto addUserDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };

            var checkUsersAddedBefore = await _unitOfWork.UserRepository.FindElementAsync(x => x.Email == addUserDto.Email || x.NumberPhone == addUserDto.NumberPhone);
            if (checkUsersAddedBefore == null)
            {
                var newUser = _mapper.Map<AddUserDto, UsersProfile>(addUserDto);
                await _unitOfWork.UserRepository.CreateAsync(newUser);
                creationState.IsCreatedSuccessfully = await _unitOfWork.SaveAsync() > 0;
                creationState.CreatedObjectId = newUser.Id;

                string extention = Path.GetExtension(addUserDto.File.FileName);
                string path = _hostEnvironment.WebRootPath + "/Uploads/" + newUser.Id + extention;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await addUserDto.File.CopyToAsync(stream);
                }
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
            if (user != null)
            {
                var userDto = _mapper.Map<UsersProfile, UserDto>(user);
                return userDto;
            }
            return null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(string include)
        {
            var user = await _unitOfWork.UserRepository.GetAllUsers(include);
            if (user != null)
            {
                var userDto = _mapper.Map<IEnumerable<UsersProfile>, IEnumerable<UserDto>>(user);
                return userDto;
            }
            return null;
        }

        public async Task<bool> LoginUser(LoginUserDto loginUserDto)
        {
            //get user that mathes email or phoneNamber and password
            var getuser = await _unitOfWork.UserRepository.FindElementAsync(x => (x.Email == loginUserDto.Email || x.NumberPhone == x.NumberPhone) && x.Password == x.Password);
            if (getuser != null)
            {
                return true;
            }
            return false;
        }

        public async Task<CreationState> AddUserAsync(AddUserDto addUserDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };
            try
            {
                //Identity User Mapping
                var newUser = _mapper.Map<AddUserDto, IdentityUser>(addUserDto);
                //Adding User
                var result = await _userManager.CreateAsync(newUser, addUserDto.Password);
                //Check  If User Added 
                if (result.Succeeded)
                {
                    var userId = Guid.Parse(await _userManager.GetUserIdAsync(newUser));
                    creationState.IsCreatedSuccessfully = true;
                    creationState.CreatedObjectId = userId;
                    if(addUserDto.File != null)
                    {
                        string extention = Path.GetExtension(addUserDto.File.FileName);
                        string path = _hostEnvironment.WebRootPath + "/Uploads/" + newUser.Id + extention;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await addUserDto.File.CopyToAsync(stream);
                        }
                    }
                    
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        creationState.ErrorMessages.Add(item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                creationState.ErrorMessages.Add(ex.Message);
            }
            return creationState;
        }
    }
}
