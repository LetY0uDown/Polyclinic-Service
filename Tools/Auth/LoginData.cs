using System.ComponentModel.DataAnnotations;

namespace Tools.Auth;

public sealed record class LoginData
{
    public LoginData (string eMail, string password, string login)
    {
        EMail = eMail;
        Password = password;
        Login = login;
    }

    [RegularExpression("^[A-Za-z0-9_-]{5,25}$", ErrorMessage = "Логин задан неправильно")]
    public string Login { get; private init; } = string.Empty;

    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,10}$", ErrorMessage = "Неверный E-Mail")]
    public string EMail { get; private init; } = string.Empty;

    [RegularExpression("^[A-Za-z0-9_@#%&()]{5,20}$", ErrorMessage = "Пароль введён неверно. И т.д. Потом придумаю")]
    public string Password { get; private init; } = string.Empty;
}