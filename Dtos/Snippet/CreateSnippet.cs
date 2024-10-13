using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.Snippet
{
    public class CreateSnippet
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        public required string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public required string Description { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "Code cannot be longer than 100000 characters.")]
        public required string Code { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Language cannot be longer than 20 characters.")]
        public required string Language { get; set; }

        [Required]
        public bool IsPublic { get; set; } = true;

        [Required]
        public required string UserId { get; set; }

        public List<string> Tags { get; set; } = [];
    }
}
