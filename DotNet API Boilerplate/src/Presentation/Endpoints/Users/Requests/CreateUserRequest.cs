namespace DotNet_API_Boilerplate.Presentation.Endpoints.Users.Requests;
using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
}
