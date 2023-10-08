using System.ComponentModel.DataAnnotations;

namespace DTO.Auth;

public sealed class LoginData
{
    [Required(ErrorMessage = "Логин не введён"),
     RegularExpression("^[A-Za-zА-Яа-я0-9_-]{5,25}$", ErrorMessage = "Логин задан неправильно")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-Mail не введён"),
     RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,10}$", ErrorMessage = "Неверный E-Mail")]
    public string EMail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль не введён"),
     RegularExpression("^[A-Za-z0-9_@#%&()]{5,20}$", ErrorMessage = "Пароль введён неверно. И т.д. Потом придумаю")]
    public string Password { get; set; } = string.Empty;
}