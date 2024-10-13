using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.Tag
{
    public class CreateTag
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public required string Name { get; set; }
    }
}
