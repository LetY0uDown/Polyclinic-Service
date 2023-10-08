 namespace DTO.Auth;

/// <summary>
/// Содержит данные, необходимые клиенту для авторизации и доступа к некоторым контроллерам
/// </summary>
public record class AuthorizationData
{
    public required Guid ID { get; init; }

    public required string JWTToken { get; init; } = null!;
}