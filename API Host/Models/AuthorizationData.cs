namespace API_Host.Models;

public record class AuthorizationData
{
    public AuthorizationData (string id, string jWTToken)
    {
        ID = id;
        JWTToken = jWTToken;
    }

    public string ID { get; private init; }

    public string JWTToken { get; private init; }
}