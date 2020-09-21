using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApp.Models.API
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Username { get; set; }
        public string Token { get; internal set; }
        public IList<string> Roles { get; internal set; }
    }
}
