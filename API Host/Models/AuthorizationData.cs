namespace API_Host.Models;

public record class AuthorizationData
{
    public AuthorizationData (string ID, string jWTToken)
    {
        ID = ID;
        JWTToken = jWTToken;
    }

    public string ID { get; private init; }

    public string JWTToken { get; private init; }
}