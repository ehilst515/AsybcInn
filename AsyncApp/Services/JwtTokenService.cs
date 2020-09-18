using AsyncApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),

                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secet = configuration["JWT:Secret"];
            if (secet == null) throw new InvalidOperationException("JWT:Secret is missing");

            var secretBytes = Encoding.UTF8.GetBytes(secet);
            return new SymmetricSecurityKey(secretBytes);
            
        }
    }
}
