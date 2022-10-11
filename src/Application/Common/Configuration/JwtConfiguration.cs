namespace DotNet_API_Boilerplate.Core.Common.Configuration;

public class JwtConfiguration
{
    public const string Section = "Jwt";
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresIn { get; set; }
}
