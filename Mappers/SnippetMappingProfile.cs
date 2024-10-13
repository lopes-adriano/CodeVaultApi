using AutoMapper;
using CodeVaultApi.Dtos.Snippet;
using CodeVaultApi.Dtos.Tag;
using CodeVaultApi.Models;

namespace CodeVaultApi.Mappers
{

    public class SnippetMappingProfile : Profile
    {
        public SnippetMappingProfile()
        {
            CreateMap<Snippet, SnippetDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name).ToList()));

            CreateMap<CreateSnippet, Snippet>();
            CreateMap<UpdateSnippet, Snippet>();
            CreateMap<Tag, TagDto>();
        }
    }

}