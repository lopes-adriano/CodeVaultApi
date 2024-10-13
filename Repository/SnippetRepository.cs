using CodeVaultApi.Data;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Models;
using CodeVaultApi.Util;
using Microsoft.EntityFrameworkCore;

namespace CodeVaultApi.Repository
{
    public class SnippetRepository(ApplicationDbContext context) : ISnippetRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Snippet>> GetAll(QueryObject? queryObject)
        {
            var snippetsQuery = _context.Snippets.Include(snippet => snippet.Tags).AsQueryable();

            if (queryObject == null)
            {
                return await snippetsQuery.ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Query))
            {
                snippetsQuery = snippetsQuery.Where(snippet =>
                    snippet.Title.Contains(queryObject.Query, StringComparison.OrdinalIgnoreCase)
                    || snippet.Description.Contains(
                        queryObject.Query,
                        StringComparison.OrdinalIgnoreCase
                    )
                    || snippet.Tags.Any(tag =>
                        tag.Name.Contains(queryObject.Query, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }

            var pagesToSkip = (queryObject.Page - 1) * queryObject.PageSize;

            return await snippetsQuery.Skip(pagesToSkip).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Snippet?> GetById(int id)
        {
            return await _context
                .Snippets.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Snippet>> GetUserSnippets(string userId)
        {
            return await _context
                .Snippets.Include(x => x.Tags)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Snippet> CreateSnippet(Snippet snippet)
        {
            await _context.Snippets.AddAsync(snippet);
            await _context.SaveChangesAsync();
            return snippet;
        }

        public async Task<Snippet?> UpdateSnippet(int id, Snippet snippet)
        {
            var snippetToUpdate = await GetById(id);
            if (snippetToUpdate == null)
                return null;
            _context.Entry(snippetToUpdate).CurrentValues.SetValues(snippet);
            await _context.SaveChangesAsync();
            return snippetToUpdate;
        }

        public async Task<Snippet?> DeleteSnippet(int id)
        {
            var snippet = await GetById(id);
            if (snippet == null)
                return null;
            _context.Snippets.Remove(snippet);
            await _context.SaveChangesAsync();
            return snippet;
        }

        public async Task<bool> SnippetExists(int id)
        {
            return await _context.Snippets.AnyAsync(x => x.Id == id);
        }
    }
}
