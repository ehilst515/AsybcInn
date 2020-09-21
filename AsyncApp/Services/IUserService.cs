using AsyncApp.Models;
using AsyncApp.Models.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AsyncApp.Services
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState);

        Task<UserDto> Authenticate(string username, string password);

        Task<UserDto> GetUser(ClaimsPrincipal user);
    }

    class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtTokenService tokenService;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JwtTokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5)),
                };
            }

            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = await userManager.GetRolesAsync(user),
            };
        }

        public async Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,

            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                if(data.Roles?.Any() == true)
                {
                    await userManager.AddToRolesAsync(user, data.Roles);
                }
                else
                {
                    //defualt role
                }
               
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5)),
                    Roles = await userManager.GetRolesAsync(user),
                };
            };


            foreach(var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("Password") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
