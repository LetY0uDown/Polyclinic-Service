using API_Host.Models;
using API_Host.Services.Interfaces;
using System.Text.RegularExpressions;

namespace API_Host.Services;

public class LoginDataValidator : IValidator<LoginData>
{
    private readonly IConfiguration _configuration;

    public LoginDataValidator (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsValid (LoginData obj, out string error)
    {
        if (obj.IsEmpty) {
            error = "Недостаточно данных. Введите ваш E-Mail и пароль";
            return false;
        }

        var regex = new Regex(_configuration["Regex:Email"]!);

        if (!regex.IsMatch(obj.EMail)) {
            error = "E-Mail адресс введён некорректно. Проверьте правильность ввода";
            return false;
        }

        error = string.Empty;

        return true;
    }
}