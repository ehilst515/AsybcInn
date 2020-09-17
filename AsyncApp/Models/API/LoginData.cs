using System.ComponentModel.DataAnnotations;

namespace AsyncApp.Models.API
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
