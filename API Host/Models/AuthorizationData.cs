namespace API_Host.Models;

public record class AuthorizationData
{
    public AuthorizationData (string Id, string jwtToken)
    {
        ID = Id;
        JWTToken = jwtToken;
    }

    public string ID { get; private init; }

    public string JWTToken { get; private init; }
}