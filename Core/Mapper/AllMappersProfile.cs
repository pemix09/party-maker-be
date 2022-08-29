using AutoMapper;
using Core.Dto;
using Core.Models;

namespace Core.Mapper
{
    public class AllMappersProfile : Profile
    {
        public AllMappersProfile()
        {
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
