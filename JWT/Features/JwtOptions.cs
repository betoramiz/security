namespace JWT.Features;

public class JwtOptions
{
    public const string Options = "Jwt";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}
