using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;
using Tools.Services.Interfaces;

namespace Tools.Services;

/// <summary>
/// Класс, который реализует интерфейс для шифрования строк
/// </summary>
public sealed class StringHasher : IStringHasher
{
    public string Hash (string value, string salt)
    {
        var saltBytes = Encoding.UTF8.GetBytes(salt);

        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: value,
                        salt: saltBytes,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 16
                    ));

        return hash;
    }
}