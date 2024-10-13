using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.AppUser
{
    public class RegisterUser
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long.")]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password is too long.")]
        public required string Password { get; set; }
    }
}
