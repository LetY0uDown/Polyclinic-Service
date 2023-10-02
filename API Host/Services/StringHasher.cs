using API_Host.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace API_Host.Services;

/// <summary>
/// Класс, который реализует интерфейс для шифрования строк
/// </summary>
public sealed class StringHasher : IStringHasher
{
    private readonly IConfiguration _configuration;

    public StringHasher (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Hash (string str)
    {
        var saltBytes = Encoding.UTF8.GetBytes(_configuration["Salt:Password"]!);

        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: str,
                        salt: saltBytes,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 16
                    ));

        return hash;
    }
}