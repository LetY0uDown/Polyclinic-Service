 namespace DTO.Auth;

/// <summary>
/// Содержит данные, необходимые клиенту для авторизации и доступа к некоторым контроллерам
/// </summary>
public record class AuthorizationData
{
    public AuthorizationData (string Id)
    {
        ID = Id;
    }
    
    public AuthorizationData (string Id, string jwtToken)
    {
        ID = Id;
        JWTToken = jwtToken;
    }

    public string ID { get; private init; }

    public string? JWTToken { get; private init; } = null!;
}