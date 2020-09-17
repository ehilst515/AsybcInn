using AsyncApp.Models;
using AsyncApp.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApp.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Register(RegisterData data);
    }

    class IdentityUserService : IUserService
    {
        public Task<ApplicationUser> Register(RegisterData data)
        {
            throw new NotImplementedException();
        }
    }
}
