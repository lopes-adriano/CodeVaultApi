using CodeVaultApi.Data;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeVaultApi.Repository
{
    public class TagRepository(ApplicationDbContext context) : ITagRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Tag>> GetAll()
        {
            return await _context.Tags.Include(x => x.Snippets).ToListAsync();
        }

        public async Task<Tag?> GetById(int id)
        {
            return await _context
                .Tags.Include(x => x.Snippets)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> UpdateTag(int id, Tag tag)
        {
            var tagToUpdate = await GetById(id);
            if (tagToUpdate == null)
                return null;
            tagToUpdate.Name = tag.Name;
            await _context.SaveChangesAsync();
            return tagToUpdate;
        }

        public async Task<Tag?> DeleteTag(int id)
        {
            var tag = await GetById(id);
            if (tag == null)
                return null;
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
    }
}
