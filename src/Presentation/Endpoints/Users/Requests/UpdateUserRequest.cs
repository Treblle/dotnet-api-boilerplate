namespace DotNet_API_Boilerplate.Presentation.Endpoints.Users.Requests;
using System.ComponentModel.DataAnnotations;

public class UpdateUserRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; init; }
    [Required(ErrorMessage = "New password is required")]
    public string NewPassword { get; init; }
}
