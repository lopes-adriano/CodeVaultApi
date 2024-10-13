namespace CodeVaultApi.Models
{
    public class Snippet
    {
        public required int Id { get; set; }

        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public required string Code { get; set; }

        public required string Language { get; set; }

        public required bool IsPublic { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required string UserId { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];
    }
}
