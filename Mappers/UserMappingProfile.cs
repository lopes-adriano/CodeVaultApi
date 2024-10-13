using AutoMapper;
using CodeVaultApi.Dtos.AppUser;
using CodeVaultApi.Models;

namespace CodeVaultApi.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUser, AppUser>();
        }
    }
}
