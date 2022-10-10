namespace Treblle_Core_API_Boilerplate.Core.Posts;
using AutoMapper;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;
using Treblle_Core_API_Boilerplate.Core.Posts.Entities;

internal class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostDto>()
            .ReverseMap();
    }
}
