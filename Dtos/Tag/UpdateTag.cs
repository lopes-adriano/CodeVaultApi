using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.Tag
{
    public class UpdateTag
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public required string Name { get; set; }
    }
}
