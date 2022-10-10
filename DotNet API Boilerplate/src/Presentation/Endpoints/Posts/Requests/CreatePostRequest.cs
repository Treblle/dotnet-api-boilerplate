namespace DotNet_API_Boilerplate.Presentation.Endpoints.Posts.Requests;
using System.ComponentModel.DataAnnotations;

public class CreatePostRequest
{
    [Required]
    public string Title { get; init; }
    public string Content { get; init; }
}
