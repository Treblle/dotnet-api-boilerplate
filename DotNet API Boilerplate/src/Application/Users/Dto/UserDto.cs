namespace Treblle_Core_API_Boilerplate.Core.Users.Dto;

using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

public record UserDto
{
    public Guid Uuid { get; init; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }

    public ICollection<PostDto> Posts { get; set; }
}
