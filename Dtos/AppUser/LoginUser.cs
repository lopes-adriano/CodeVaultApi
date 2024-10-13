using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.AppUser
{
    public class LoginUser
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
