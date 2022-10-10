namespace DotNet_API_Boilerplate.Core.Posts;
using AutoMapper;
using DotNet_API_Boilerplate.Core.Posts.Dto;
using DotNet_API_Boilerplate.Core.Posts.Entities;

internal class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostDto>()
            .ReverseMap();
    }
}
