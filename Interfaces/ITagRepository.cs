using CodeVaultApi.Models;

namespace CodeVaultApi.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();
        Task<Tag?> GetById(int id);
        Task<Tag> CreateTag(Tag tag);
        Task<Tag?> UpdateTag(int id, Tag tag);
        Task<Tag?> DeleteTag(int id);
    }
}
