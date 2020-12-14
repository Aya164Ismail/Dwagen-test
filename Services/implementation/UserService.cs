using AutoMapper;
using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Model.Entities;
using Dwagen.Repository;
using Dwagen.Repository.UnitOfWork;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment
                            , UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<CreationState> AddUserAsnc(AddUserDto addUserDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };

            var checkUsersAddedBefore = await _unitOfWork.UserRepository.FindElementAsync(x => x.Email == addUserDto.Email || x.PhoneNumber == addUserDto.PhoneNumber);
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
            var getuser = await _unitOfWork.UserRepository.FindElementAsync(x => (x.Email == loginUserDto.Email || x.PhoneNumber == x.PhoneNumber) && x.Password == x.Password);
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
                var newUserIdentity = _mapper.Map<AddUserDto, IdentityUser>(addUserDto);
                var checkUsersAddedBefore = await _unitOfWork.UserRepository.FindElementAsync(x => x.Email == addUserDto.Email || x.PhoneNumber == addUserDto.PhoneNumber);
                if (checkUsersAddedBefore == null)
                {
                    //Adding User
                    var result = await _userManager.CreateAsync(newUserIdentity, addUserDto.Password);
                    //Check  If User Added 
                    if (result.Succeeded)
                    {
                        var userId = Guid.Parse(await _userManager.GetUserIdAsync(newUserIdentity));
                        var newUser = _mapper.Map<AddUserDto, UsersProfile>(addUserDto);
                        newUser.Id = userId;
                        await _unitOfWork.UserRepository.CreateAsync(newUser);
                        creationState.IsCreatedSuccessfully = await _unitOfWork.SaveAsync() > 0;
                        creationState.CreatedObjectId = newUser.Id;

                        creationState.CreatedObjectId = userId;
                        if (addUserDto.File != null)
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
                else
                {
                    creationState.ErrorMessages.Add("This user added before");
                }
            }
            catch (Exception ex)
            {
                creationState.ErrorMessages.Add(ex.Message);
            }
            return creationState;
        }

        public async Task<LoginStateDto> LoginAsync(LoginUserDto loginModel)
        {
            var loginState = new LoginStateDto { LoginSuccessfully = false, Token = null };

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
           
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, true, false);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                          new Claim(ClaimTypes.Name, user.UserName),
                          new Claim("ProfileId", user.Id.ToString()),
                          new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        var role = await _roleManager.FindByNameAsync(userRole);
                        var roleclaims = await _roleManager.GetClaimsAsync(role);
                        foreach (var item in roleclaims)
                        {
                            authClaims.Add(new Claim(item.Type, item.Value));
                        }
                    }
                    //var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    //var key = new SymmetricSecurityKey(secretBytes);
                    //var algorithm = SecurityAlgorithms.HmacSha256;

                    //var signingCredentials = new SigningCredentials(key, algorithm);

                    //var token = new JwtSecurityToken(
                    //    Constants.Issuer,
                    //    Constants.Audiance,
                    //    authClaims,
                    //    notBefore: DateTime.Now,
                    //    expires: DateTime.Now.AddDays(2),
                    //    signingCredentials);


                    //var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                    //loginState.LoginSuccessfully = true;
                    //loginState.Token = tokenJson;
                    //loginState.Permissions = authClaims;
                    //loginState.ErrorMessage = null;
                }
            }
            return loginState;
        }
    }
}
