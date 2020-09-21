using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsyncApp.Models.API;
using AsyncApp.Services;
using AsyncApp.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterData data)
        {
            UserDto user = await userService.Register(data, this.ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData data)
        {
            var user = await userService.Authenticate(data.Username, data.Password);

            if (user == null)
                return Unauthorized();

            return user;
        }


        [HttpGetAttribute("Self")]
        public async Task<ActionResult<UserDto>> Self()
        {
            return await userService.GetUser(this.User);
        }

    }
}
