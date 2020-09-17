using AsyncApp.Models;
using AsyncApp.Models.API;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;


namespace AsyncApp.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Register(RegisterData data);
    }

    class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ApplicationUser> Register(RegisterData data)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,

            };

            await userManager.CreateAsync(user, data.Password);

            return user;

        }
    }
}
