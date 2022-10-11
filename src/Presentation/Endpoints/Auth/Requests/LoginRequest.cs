namespace DotNet_API_Boilerplate.Presentation.Endpoints.Auth.Requests;
using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
}
