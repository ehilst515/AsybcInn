using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncApp.Models;
using AsyncApp.Models.API;
using AsyncApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AsyncApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterData data)
        {
            ApplicationUser user = await userService.Register(data);
            return user;
        }
    }
}
