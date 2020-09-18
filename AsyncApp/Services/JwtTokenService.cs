using AsyncApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AsyncApp.Services
{
    public class JwtTokenService
    {
        private IConfiguration configuration;
        private SignInManager<ApplicationUser> signInManager;

        public JwtTokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;

        }
    }
}
