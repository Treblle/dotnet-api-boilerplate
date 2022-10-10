namespace DotNet_API_Boilerplate.Core.Auth.Dto;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
}
