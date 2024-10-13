namespace CodeVaultApi.Dtos.Snippet
{
    public class SnippetDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Code { get; set; }

        public required string Language { get; set; }

        public required bool IsPublic { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }

        public List<string> Tags { get; set; } = [];
    }
}
