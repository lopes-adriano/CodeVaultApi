namespace CodeVaultApi.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Snippet> Snippets { get; set; } = [];
    }

}