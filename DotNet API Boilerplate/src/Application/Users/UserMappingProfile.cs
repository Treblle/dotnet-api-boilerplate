namespace DotNet_API_Boilerplate.Core.Users;
using AutoMapper;
using DotNet_API_Boilerplate.Core.Users.Dto;
using DotNet_API_Boilerplate.Core.Users.Entities;

internal class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(d => d.Uuid, opt => opt.MapFrom(s => Guid.Parse(s.Id)))
            .ForMember(s => s.Posts, o => o.ExplicitExpansion())
            .ReverseMap();
    }
}
