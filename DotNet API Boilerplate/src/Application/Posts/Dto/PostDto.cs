namespace DotNet_API_Boilerplate.Core.Posts.Dto;

using DotNet_API_Boilerplate.Core.Common.Dto;
using DotNet_API_Boilerplate.Core.Users.Dto;

public record PostDto : EntityDto
{
    public string Title { get; init; }
    public string Content { get; init; }
    public UserDto User { get; init; }
}
