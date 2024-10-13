using AutoMapper;
using CodeVaultApi.Dtos.Snippet;
using CodeVaultApi.Extensions;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Models;
using CodeVaultApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeVaultApi.Controllers
{
    [Route("api/snippets")]
    [ApiController]
    public class SnippetController(
        IMapper mapper,
        ISnippetRepository snippetRepo,
        UserManager<AppUser> userManager
    ) : ControllerBase
    {
        private readonly ISnippetRepository _snippetRepo = snippetRepo;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> GetAllSnippets([FromQuery] QueryObject? query)
        {
            var snippets = await _snippetRepo.GetAll(query);

            var snippetDtos = _mapper.Map<List<SnippetDto>>(snippets);

            return Ok(snippetDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var snippet = await _snippetRepo.GetById(id);
            return snippet == null ? NotFound() : Ok(_mapper.Map<SnippetDto>(snippet));
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserSnippets([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound();

            var snippets = await _snippetRepo.GetUserSnippets(user.Id);
            return snippets == null ? NotFound() : Ok(_mapper.Map<List<SnippetDto>>(snippets));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSnippet([FromBody] CreateSnippet createSnippet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(User.GetUsername());

            if (user == null)
                return Unauthorized("Você precisa estar logado para criar um snippet");

            var snippet = _mapper.Map<Snippet>(createSnippet);
            snippet.UserId = user.Id;
            await _snippetRepo.CreateSnippet(snippet);

            return CreatedAtAction(
                nameof(GetById),
                new { id = snippet.Id },
                _mapper.Map<SnippetDto>(snippet)
            );
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateSnippet(
            [FromRoute] int id,
            [FromBody] UpdateSnippet snippet
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(User.GetUsername());

            if (user == null)
                return Unauthorized("Você precisa estar logado para atualizar um snippet");

            var snippetToUpdate = await _snippetRepo.GetById(id);

            if (snippetToUpdate == null || snippetToUpdate.UserId != user.Id)
                return Forbid("Você apenas pode atualizar seus próprios snippets");

            _mapper.Map(snippet, snippetToUpdate);
            await _snippetRepo.UpdateSnippet(id, snippetToUpdate);
            if (snippetToUpdate == null)
                return NotFound();

            return Ok(_mapper.Map<SnippetDto>(snippetToUpdate));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteSnippet([FromRoute] int id)
        {
            var user = await _userManager.FindByNameAsync(User.GetUsername());

            if (user == null)
                return Unauthorized("Você precisa estar logado para deletar um snippet");

            var snippetToDelete = await _snippetRepo.GetById(id);

            if (snippetToDelete == null || snippetToDelete.UserId != user.Id)
                return Forbid("Você apenas pode deletar seus próprios snippets");

            var deletedSnippet = await _snippetRepo.DeleteSnippet(id);
            if (deletedSnippet == null)
                return NotFound();

            return NoContent();
        }
    }
}
