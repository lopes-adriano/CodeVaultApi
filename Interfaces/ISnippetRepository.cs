using CodeVaultApi.Models;
using CodeVaultApi.Util;

namespace CodeVaultApi.Interfaces
{
    public interface ISnippetRepository
    {
        Task<List<Snippet>> GetAll(QueryObject? queryObject);
        Task<Snippet?> GetById(int id);
        Task<List<Snippet>> GetUserSnippets(string userId);
        Task<Snippet> CreateSnippet(Snippet snippet);
        Task<Snippet?> UpdateSnippet(int id, Snippet snippet);
        Task<Snippet?> DeleteSnippet(int id);
        Task<bool> SnippetExists(int id);
    }
}
