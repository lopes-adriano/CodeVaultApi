using System.ComponentModel.DataAnnotations;

namespace CodeVaultApi.Dtos.Snippet
{
    public class UpdateSnippet
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title is too long.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        public required string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description is too long.")]
        public required string Description { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "Code is too long.")]
        public required string Code { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Language is too long.")]
        public required string Language { get; set; }

        [Required]
        public required bool IsPublic { get; set; }

        [Required]
        public List<string> Tags { get; set; } = [];
    }
}
