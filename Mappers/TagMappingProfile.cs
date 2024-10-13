using AutoMapper;
using CodeVaultApi.Dtos.Tag;
using CodeVaultApi.Models;

namespace CodeVaultApi.Mappers
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<CreateTag, Tag>();
            CreateMap<UpdateTag, Tag>();
        }
    }
}
