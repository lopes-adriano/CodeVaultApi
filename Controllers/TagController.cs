using AutoMapper;
using CodeVaultApi.Dtos.Tag;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeVaultApi.Controllers
{
    [Route("[controller]")]
    public class TagController(IMapper mapper, ITagRepository tagRepo) : ControllerBase
    {
        private readonly ITagRepository _tagRepo = tagRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagRepo.GetAll();
            return Ok(_mapper.Map<List<TagDto>>(tags));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tag = await _tagRepo.GetById(id);
            return tag == null ? NotFound() : Ok(_mapper.Map<TagDto>(tag));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagToCreate = _mapper.Map<Tag>(tag);
            await _tagRepo.CreateTag(tagToCreate);
            return CreatedAtAction(
                nameof(GetById),
                new { id = tagToCreate.Id },
                _mapper.Map<TagDto>(tagToCreate)
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromBody] UpdateTag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagToUpdate = _mapper.Map<Tag>(tag);
            var updatedTag = await _tagRepo.UpdateTag(id, tagToUpdate);
            if (updatedTag == null)
                return NotFound();

            return Ok(_mapper.Map<TagDto>(updatedTag));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            var deletedTag = await _tagRepo.DeleteTag(id);
            if (deletedTag == null)
                return NotFound();
            return Ok(_mapper.Map<TagDto>(deletedTag));
        }
    }
}
